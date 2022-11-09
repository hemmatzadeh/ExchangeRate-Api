using NF.ExchangeRates.Core.HealthCheck;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateProviderHealthReader
    {
        Task<ExchangeRateProviderHealthzResult> GetHealthz();
    }
}
