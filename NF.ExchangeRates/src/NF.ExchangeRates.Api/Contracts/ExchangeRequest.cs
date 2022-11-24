using Azure.Core;
using NF.ExchangeRates.Core.Enums;

namespace NF.ExchangeRates.Api.Contracts
{
    public record ExchangeRequest(int UserId, string From, string To, ApiProviders ApiProvider, decimal Amount);
}