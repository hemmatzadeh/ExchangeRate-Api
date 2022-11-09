using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using NF.ExchangeRates.Core.HealthCheck;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Api.HealthChecks
{
    public class ExchangeRateProviderHealthCheck : IHealthCheck
    {
        private HealthCheckResult lastResult;
        private readonly IExchangeRateProviderHealthReader _exchangeProviderHealthReader;
        public ExchangeRateProviderHealthCheck(IExchangeRateProviderHealthReader exchangeReader) => _exchangeProviderHealthReader = exchangeReader;
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {

            try
            {
                var data = await _exchangeProviderHealthReader.GetHealthz();
                var lst = data.ToDictionary();

                if (data.ServiceHealthy)
                {
                    lastResult = HealthCheckResult.Healthy("Provider is healthy", data: lst);
                }
                else
                {
                    lastResult = HealthCheckResult.Unhealthy("Provider is unhealthy", data: lst);
                }
            }
            catch (Exception ex)
            {
                lastResult = HealthCheckResult.Unhealthy(ex.Message);
            }
            return lastResult;
        }

    }

    public static class HealthzResultExtensions
    {
        public static Dictionary<string, object> ToDictionary(this ExchangeRateProviderHealthzResult result)
        {
            var json = JsonConvert.SerializeObject(result);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            return dictionary;
        }
    }
}
