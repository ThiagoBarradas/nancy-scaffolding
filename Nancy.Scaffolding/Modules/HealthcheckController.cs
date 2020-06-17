using Nancy.Scaffolding.Healthcheck;
using Nancy.Scaffolding.Models;
using Nancy.Serilog.Simple.Extensions;
using Nancy.TinyIoc;
using System.Collections.Generic;

namespace Nancy.Scaffolding.Modules
{
    public class HealthcheckController : BaseModule
    {
        private HealthcheckSettings HealthcheckSettings { get; set; }

        private List<IHealthcheck> Healthchecks { get; set; }

        public HealthcheckController(HealthcheckSettings healthcheckSettings, TinyIoCContainer container)
        {
            this.HealthcheckSettings = healthcheckSettings;

            if (healthcheckSettings?.Enabled == true)
            {
                this.Get(healthcheckSettings.Path, args => this.GetHealthcheck(), name: nameof(this.GetHealthcheck));
                this.Healthchecks = Api.ApiBasicConfiguration?.ConfigureHealthcheck?.Invoke(container);
            }
        }

        public object GetHealthcheck()
        {
            if (this.HealthcheckSettings.LogEnabled == false)
            {
                this.DisableLogging();
            }

            return this.CreateJsonResponse(HealthcheckProcessor.Verify(this.Healthchecks));
        }
    }
}
