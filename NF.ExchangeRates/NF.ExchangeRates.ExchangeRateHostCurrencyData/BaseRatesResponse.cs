namespace NF.ExchangeRates.ExchangeRateHostCurrencyData
{
    public class BaseRatesResponse
    {
        public bool Success { get; set; }
        public DateTime Date { get; set; }
        public decimal Result { get; set; }
        public Dictionary<string, string> Query { get; set; }
        public Dictionary<string, decimal> Info { get; set; }

        public ExchangeRateHostErrorResponse Error { get; set; }
    }
}
