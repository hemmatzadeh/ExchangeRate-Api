using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateWriter
    {
        Task Write(
            ApiProviders provider,
            string from,
            string to,
            decimal rate,
            CancellationToken cancellationToken = default);

        Task WriteAll(ApiProviders provider,string from, Dictionary<string, decimal> quotes, CancellationToken cancellationToken = default);
    }
}
