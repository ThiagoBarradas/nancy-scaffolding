using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Nancy.Scaffolding.Models;
using System;
using System.IO;

namespace Nancy.Scaffolding
{
    public static class Api
    {
        public static ApiSettings ApiSettings { get; set; } = new ApiSettings();

        public static LogSettings LogSettings { get; set; } = new LogSettings();

        public static DbSettings DbSettings { get; set; } = new DbSettings();

        public static DocsSettings DocsSettings { get; set; } = new DocsSettings();

        public static ApiBasicConfiguration ApiBasicConfiguration { get; set; } = new ApiBasicConfiguration();

        public static IConfigurationRoot ConfigurationRoot { get; set; }

        public static void Run(ApiBasicConfiguration apiBasicConfiguration)
        {
            ApiBasicConfiguration = apiBasicConfiguration;

            Console.WriteLine("{0} is running...", ApiBasicConfiguration.ApiName);

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://*:" + ApiBasicConfiguration.ApiPort.ToString())
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
