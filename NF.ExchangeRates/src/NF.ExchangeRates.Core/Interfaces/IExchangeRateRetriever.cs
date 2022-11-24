using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateRetriever
    {
        Task<ExchangeRate> GetExchangeRate(ApiProviders provider, string from, string to, CancellationToken cancellationToken);

    }
}
