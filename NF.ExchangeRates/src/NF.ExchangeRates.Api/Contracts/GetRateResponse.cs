namespace NF.ExchangeRates.Api.Contracts
{
    public record GetRateResponse(string From, string To, decimal Rate, DateTime Created);    
    
}
