using Nancy.Serilog.Simple.Extensions;
using System;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.Modules
{
    public class DefaultHomeController : BaseModule
    {
        public DefaultHomeController()
        {
            this.Get("", args => this.Home(), name: nameof(this.Home));
        }

        public object Home()
        {
            this.DisableLogging();

            return this.CreateJsonResponse(ApiResponse.OK(new HomeDetails
            {
                Service = Api.ApiBasicConfiguration?.ApiName,
                BuildVersion = Api.ApiSettings?.BuildVersion,
                Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"),
                Application = Api.ApiSettings.Application,
                Domain = Api.ApiSettings.Domain,
                EnvironmentPrefix = Api.ApiBasicConfiguration.EnvironmentVariablesPrefix,
            }));
        }
    }

    public class HomeDetails
    {
        public string Framework { get; set; } = "NancyFx";

        public string Service { get; set; }

        public string BuildVersion { get; set; }

        public string Environment { get; set; }

        public string Application { get; set; }

        public string Domain { get; set; }

        public string EnvironmentPrefix { get; set; }
    }
}
