using NF.ExchangeRates.Core.Entities;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IRateRepository
    {
        Task<RateInfo> GetAsync(string baseCurrency, string toCurrency, CancellationToken cancellationToken);
        Task SaveRateAsync(string from,string to,decimal rate, CancellationToken cancellationToken);
        Task SaveRatesAsync(string from, Dictionary<string, decimal> Quotes, CancellationToken cancellationToken);

    }
}
