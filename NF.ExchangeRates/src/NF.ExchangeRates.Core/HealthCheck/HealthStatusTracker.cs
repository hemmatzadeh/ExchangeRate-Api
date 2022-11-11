using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace NF.ExchangeRates.Core.HealthCheck
{
    public sealed class HealthStatusTracker
    {
        public HealthStatus Current { get; set; }
        public HealthStatus Previous { get; set; }
        public int Count { get; set; }
    }
}
