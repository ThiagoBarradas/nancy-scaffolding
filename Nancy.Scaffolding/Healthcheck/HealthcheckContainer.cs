using System.Collections.Generic;
using System.Linq;

namespace Nancy.Scaffolding.Healthcheck
{
    public class HealthcheckContainer
    {
        public HealthcheckContainer()
        {
            this.Results = new Dictionary<string, HealthcheckResult>();
        }

        public string Status => (this.Results?.Any(r => r.Value.Status == "unhealthy") == true)
            ? "unhealthy" : "healthy";

        public Dictionary<string, HealthcheckResult> Results { get; set; }
    }

    public class HealthcheckResult
    {
        public string Status { get; set; }

        public string Description { get; set; }
    }
}
