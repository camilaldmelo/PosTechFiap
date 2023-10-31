using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace API.HealthCheck
{
    public class MemoriaHealthCheck : IHealthCheck
    {
        const long THRESHOLD = 1024L;

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var allocated = GC.GetTotalMemory(forceFullCollection: false);
            var data = new Dictionary<string, object>()
            {
                { "Allocated", allocated },
            };

            var result = allocated >= THRESHOLD ? context.Registration.FailureStatus : HealthStatus.Healthy;

            return Task.FromResult(new HealthCheckResult(
                result,
                description: "reports degraded status if allocated bytes >= 1mb",
                data: data));
        }
    }
}
