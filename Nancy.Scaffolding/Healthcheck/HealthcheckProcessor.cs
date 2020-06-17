using System.Collections.Generic;
using System.Linq;
using WebApi.Models.Response;

namespace Nancy.Scaffolding.Healthcheck
{
    public class HealthcheckProcessor
    {
        public static ApiResponse Verify(List<IHealthcheck> healthchecks)
        {
            var result = new HealthcheckContainer();

            if (healthchecks?.Any() == true)
            {
                foreach (var hc in healthchecks)
                {
                    var (success, description) = hc.IsHealth();

                    result.Results.Add(hc.Name, new HealthcheckResult
                    {
                        Status = (success) ? "healthy" : "unhealthy",
                        Description = description
                    });
                }
            }

            var response = ApiResponse.OK(result);

            if (result.Status == "unhealthy")
            {
                response.StatusCode = System.Net.HttpStatusCode.ServiceUnavailable;
            }

            return response;
        }
    }
}
