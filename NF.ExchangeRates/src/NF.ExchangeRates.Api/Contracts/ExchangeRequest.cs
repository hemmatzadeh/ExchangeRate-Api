namespace NF.ExchangeRates.Api.Contracts
{
    public record ExchangeRequest(int UserId, string From, string To,decimal Amount);    
}