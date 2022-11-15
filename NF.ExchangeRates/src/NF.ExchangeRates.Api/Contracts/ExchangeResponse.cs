namespace NF.ExchangeRates.Api.Contracts
{
    public record ExchangeResponse(string Message, decimal ExchangeRate, decimal ConvertedTotal);	
}
