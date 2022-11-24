using NF.ExchangeRates.Core.Entities;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IRateRepository
    {
        Task<RateInfo> GetAsync(short provider, string baseCurrency, string toCurrency, CancellationToken cancellationToken);
        Task SaveRateAsync(short provider, string from,string to,decimal rate, CancellationToken cancellationToken);
        Task SaveRatesAsync(short provider, string from, Dictionary<string, decimal> Quotes, CancellationToken cancellationToken);

    }
}
