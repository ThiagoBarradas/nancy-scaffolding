using AutoMapper;
using Nancy.Bootstrapper;
using Nancy.Scaffolding.Models;
using Nancy.TinyIoc;

namespace Nancy.Scaffolding.ApiDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ApiBasicConfiguration
            {
                ApplicationContainer = RegisterApplicationContainer,
                RequestContainer = RegisterRequestContainer,
                Pipelines = ConfigurePipelines,
                Mapper = ConfigureMapper,
                ApiName = "My Apppp",
                ApiPort = 5855,
                EnvironmentVariablesPrefix = "Prefix_"
            };

            Api.Run(config);

        }

        public static TinyIoCContainer RegisterApplicationContainer(TinyIoCContainer container)
        {
            return container;
        }

        public static TinyIoCContainer RegisterRequestContainer(NancyContext context, TinyIoCContainer container)
        {
            return container;
        }

        public static IPipelines ConfigurePipelines(IPipelines pipelines, TinyIoCContainer container)
        {
            return pipelines;
        }

        public static IMapperConfigurationExpression ConfigureMapper(IMapperConfigurationExpression config, TinyIoCContainer container)
        {
            return config;
        }
    }

    // como fazer upload de imagens?
    // como retornar imagens?

    // automapper
    // testar : por __ environment
    // documentação swagger




    // rabbit ??
    // eventsourcing

    // app manage API
    // accounts API
    // app auth API
    // proxy API
    // trial API
    // image API
    // authorize app DLL
    // authorize user API / DLL

    // DONE
    // json serializer nancy

    // mongocrud
    // pagedlist-netcore
    // restasharp-autolog
    // i18n with resources
    // fluent validation
}
