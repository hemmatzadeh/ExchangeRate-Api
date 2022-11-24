using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IGetExchangeRate
    {
        Task<ExchangeRate> Execute(ApiProviders provider, string from, string to, CancellationToken cancellationToken = default);
    }
}
