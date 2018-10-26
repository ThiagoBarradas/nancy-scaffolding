using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Nancy.Owin;
using Serilog;
using System.IO;

namespace Nancy.Scaffolding
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var envName = env.EnvironmentName;
            
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables(Api.ApiBasicConfiguration.EnvironmentVariablesPrefix);

            Api.ConfigurationRoot = builder.Build();
           
            Api.ConfigurationRoot.GetSection("ApiSettings").Bind(Api.ApiSettings);
            Api.ConfigurationRoot.GetSection("LogSettings").Bind(Api.LogSettings);
            Api.ConfigurationRoot.GetSection("DbSettings").Bind(Api.DbSettings);
            Api.ConfigurationRoot.GetSection("DocsSettings").Bind(Api.DocsSettings);
        }

        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilog();

            app.UseOwin(owin => owin.UseNancy(new NancyOptions
            {
                Bootstrapper = new Bootstrapper()
            }));
        }
    }
}
