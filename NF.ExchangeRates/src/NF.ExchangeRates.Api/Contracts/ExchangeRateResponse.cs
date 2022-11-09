namespace NF.ExchangeRates.Api.Contracts
{
    public class ExchangeRateResponse
    {
        public string From { get; set; }
        public DateTime Created { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}
