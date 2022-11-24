using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateReader
    {
        Task<ExchangeRate> Read(ApiProviders apiProvider, string from, string to, CancellationToken cancellationToken = default);
    }
}
