using AutoMapper;
using Microsoft.Extensions.Configuration;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.ErrorHandling;
using Nancy.Scaffolding.Docs;
using Nancy.Scaffolding.Enums;
using Nancy.Scaffolding.Handlers;
using Nancy.Scaffolding.Mappers;
using Nancy.Scaffolding.Models;
using Nancy.Scaffolding.Modules;
using Nancy.Serilog.Simple;
using Nancy.Serilog.Simple.Extensions;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using PackUtils;
using RestSharp.Serilog.Auto;
using Serilog;
using Serilog.Builder;
using System;
using System.Globalization;
using System.Linq;

namespace Nancy.Scaffolding
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        private IPipelines Pipelines { get; set; }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            this.Pipelines = pipelines;
            this.EnableCors(pipelines);
            this.EnableCSRF(pipelines);
            this.SetupLogger(pipelines, container);
            this.AddRequestKey(pipelines, container);
            this.SetupMapper(container);
            Api.ApiBasicConfiguration.Pipelines?.Invoke(pipelines, container);
            SwaggerConfiguration.Register(container.Resolve<JsonSerializerSettings>());
            Api.ApiBasicConfiguration.ApplicationStartup?.Invoke(pipelines, container);
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            this.RegisterAssemblies(container);

            container.Register(Api.ApiSettings);
            container.Register(Api.LogSettings);
            container.Register(Api.DbSettings);
            container.Register(Api.DocsSettings);
            
            container.Register<ICommunicationLogger, CommunicationLogger>();
            container.Register<IStatusCodeHandler, StatusCodeHandler>().AsSingleton();
            container.Register<IConfigurationRoot>(Api.ConfigurationRoot);
            container.Register<IRestClientFactory, RestClientFactory>();

            this.RegisterJsonSettings(container);
            this.RegisterCultureSettings(container);

            Api.ApiBasicConfiguration?.ApplicationContainer?.Invoke(container);
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);

            var additionalInfo = new AdditionalInfo();
            context.Items["NancySerilogAdditionalInfo"] = additionalInfo;
            container.Register<AdditionalInfo>(additionalInfo);

            this.RegisterCurrentCulture(context, container);

            Api.ApiBasicConfiguration?.RequestContainer?.Invoke(context, container);
        }

        protected override void ConfigureConventions(NancyConventions conventions)
        {
            base.ConfigureConventions(conventions);
        }

        protected void RegisterAssemblies(TinyIoCContainer container)
        {
            if (Api.ApiBasicConfiguration?.AutoRegisterAssemblies?.Any() == true)
            {
                container.AutoRegister(Api.ApiBasicConfiguration.AutoRegisterAssemblies);
            }
        }

        protected void SetupMapper(TinyIoCContainer container)
        {
            Mapper.Reset();
            Mapper.Initialize(config =>
                Api.ApiBasicConfiguration.Mapper?.Invoke(config, container));

            var mapper = new Mapper(Mapper.Configuration);

            container.Register<IRuntimeMapper>(mapper);
            container.Register<IMapper>(mapper);
            GlobalMapper.Mapper = mapper;
        }

        protected void RegisterJsonSettings(TinyIoCContainer container)
        {
            JsonSerializer jsonSerializer = null;
            JsonSerializerSettings jsonSerializerSettings = null;

            switch (Api.ApiSettings?.JsonSerializer)
            {
                case JsonSerializerEnum.Camelcase:
                    jsonSerializer = JsonUtility.CamelCaseJsonSerializer;
                    jsonSerializerSettings = JsonUtility.CamelCaseJsonSerializerSettings;
                    break;
                case JsonSerializerEnum.Lowercase:
                    jsonSerializer = JsonUtility.LowerCaseJsonSerializer;
                    jsonSerializerSettings = JsonUtility.LowerCaseJsonSerializerSettings;
                    break;
                case JsonSerializerEnum.Snakecase:
                    jsonSerializer = JsonUtility.SnakeCaseJsonSerializer;
                    jsonSerializerSettings = JsonUtility.SnakeCaseJsonSerializerSettings;
                    break;
                default:
                    break;
            }

            BaseModule.JsonSerializerSettings = jsonSerializerSettings;

            container.Register(jsonSerializer);
            container.Register(jsonSerializerSettings);
        }

        protected void RegisterCultureSettings(TinyIoCContainer container)
        {
            var defaultLanguage = Api.ApiSettings?.SupportedCultures?.FirstOrDefault() ?? "en-US";
            var supportedCultures = Api.ApiSettings?.SupportedCultures?.Select(r => r.ToLowerInvariant().Trim());

            if (supportedCultures?.Any() == false)
            {
                supportedCultures = new string[] { defaultLanguage };
            }

            GlobalizationConfiguration config = new GlobalizationConfiguration(supportedCultures, defaultLanguage);

            var defaultCulture = new CultureInfo(defaultLanguage);
            CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
            CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

            container.Register(config);
        }

        protected void RegisterCurrentCulture(NancyContext context, TinyIoCContainer container)
        {
            var config = container.Resolve<GlobalizationConfiguration>();

            var culture = new CultureInfo(config.DefaultCulture);
            var languageHeader = context.Request.Headers
                .Where(header => header.Key == "Accept-Language");

            if (languageHeader?.Any() == true)
            {
                var language = languageHeader.First().Value.First()
                                    .Split(',').First().Trim().ToLowerInvariant();

                if (config.SupportedCultureNames.Contains(language) == true)
                {
                    culture = new CultureInfo(language);
                }
            }

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;
            container.Register(culture);
            context.Culture = culture;
        }

        protected void EnableCors(IPipelines pipelines)
        {
            pipelines.AfterRequest.AddItemToStartOfPipeline((context) =>
            {
                context.Response
                       .WithHeader("Access-Control-Allow-Origin", "*")
                       .WithHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT,DELETE")
                       .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");
            });

            pipelines.OnError.AddItemToStartOfPipeline((context, ex) =>
            {
                var response = context.Response ?? new Response();

                context.Response
                      .WithHeader("Access-Control-Allow-Origin", "*")
                      .WithHeader("Access-Control-Allow-Methods", "GET,HEAD,OPTIONS,POST,PUT,DELETE")
                      .WithHeader("Access-Control-Allow-Headers", "Content-Type, Accept, Authorization");

                context.Response = response;

                return null;
            });
        }

        protected void EnableCSRF(IPipelines pipelines)
        {
            Security.Csrf.Enable(pipelines);
        }

        protected void AddRequestKey(IPipelines pipelines, TinyIoCContainer container)
        {
            pipelines.BeforeRequest.AddItemToEndOfPipeline((context) =>
            {
                context.Items.TryGetValue("RequestKey", out object requestKey);
                container.Register(new RequestKey
                {
                    Value = requestKey?.ToString()
                });

                return null;
            });
        }

        protected void SetupLogger(IPipelines pipelines, TinyIoCContainer container)
        {
            var loggerBuilder = new LoggerBuilder();

            Log.Logger = loggerBuilder
                .UseSuggestedSetting(Api.ApiSettings?.Domain, Api.ApiSettings?.Application)
                .SetupSeq(Api.LogSettings?.SeqOptions)
                .SetupSplunk(Api.LogSettings?.SplunkOptions)
                .SetupNewRelic(Api.LogSettings?.NewRelicOptions)
                .SetupGoogleCloudLogging(Api.LogSettings?.GoogleCloudLoggingOptions)
                .BuildLogger();

            var logger = container.Resolve<ICommunicationLogger>();

            logger.NancySerilogConfiguration.InformationTitle =
                Api.LogSettings?.TitlePrefix + CommunicationLogger.DefaultInformationTitle;
            logger.NancySerilogConfiguration.ErrorTitle =
                Api.LogSettings?.TitlePrefix + CommunicationLogger.DefaultErrorTitle;
            logger.NancySerilogConfiguration.Blacklist = Api.LogSettings?.JsonBlacklist;

            if (Api.LogSettings?.DebugEnabled ?? false)
            {
                loggerBuilder.EnableDebug();
            }

            pipelines.AddLogPipelines(container);
        }
    }
}