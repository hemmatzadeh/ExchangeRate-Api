namespace NF.ExchangeRates.Api.Contracts
{
    public class ExchangeRequest
    {
        public int UserId { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public decimal Amount { get; set; }
    }
}
