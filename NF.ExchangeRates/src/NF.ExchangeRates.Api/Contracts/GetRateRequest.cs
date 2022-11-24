using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Api.Contracts
{
    public record GetRateRequest(string From, string To, ApiProviders ApiProvider);   
}