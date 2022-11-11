namespace NF.ExchangeRates.Api.Contracts
{
    public class ExchangeResponse
    {
        public string Message { get; set; }
        public decimal ExchangeRate { get; set; }
        public decimal ConvertedTotal { get; set;}
    }
}
