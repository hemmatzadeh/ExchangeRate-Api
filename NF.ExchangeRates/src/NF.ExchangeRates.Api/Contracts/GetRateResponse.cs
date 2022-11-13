namespace NF.ExchangeRates.Api.Contracts
{
    public class GetRateResponse
    {
        public string From { get; set; }
        public DateTime Created { get; set; }
        public string To { get; set; }
        public decimal Rate { get; set; }
    }
}
