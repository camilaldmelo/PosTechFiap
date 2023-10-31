using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;

namespace API.HealthCheck
{
    public class EscreveHealthCheck
    {
        public Task EscreveResposta(HttpContext context, HealthReport result)
        {
            dynamic healthCheckResult = new
            {
                status = result.Status.ToString(),
                results = result.Entries.Select(entry => new
                {
                    key = entry.Key,
                    status = entry.Value.ToString(),
                    description = entry.Value.Description,
                    data = entry.Value.Data.Select(data => new { data.Key, data.Value })
                }).ToList()
            };

            string json = JsonConvert.SerializeObject(healthCheckResult, Formatting.Indented, new JsonSerializerSettings());

            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(json);
        }
    }
}
