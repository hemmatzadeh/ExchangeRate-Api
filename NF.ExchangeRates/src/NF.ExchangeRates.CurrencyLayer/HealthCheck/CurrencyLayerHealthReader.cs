using NF.ExchangeRates.Core.HealthCheck;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.CurrencyLayer.HealthCheck
{
    public class CurrencyLayerHealthReader : IExchangeRateProviderHealthReader
    {
        private readonly ICurrencyLayerHealthReader _currencyLayerHealthReader;
        public CurrencyLayerHealthReader(ICurrencyLayerHealthReader currencyLayerHealthReader) => _currencyLayerHealthReader = currencyLayerHealthReader;
        public async Task<ExchangeRateProviderHealthzResult> GetHealthz()
        {
            var result = await _currencyLayerHealthReader.GetHealthz();
            return new ExchangeRateProviderHealthzResult()
            {
                ServiceHealthy = result.Service_healthy,
                Environment = result.Environment,
                DeployDatetime = result.Deploy_datetime,
                Hostname = result.Hostname,
                Version = result.Version,
            };
        }
    }
}
