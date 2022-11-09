namespace NF.ExchangeRates.CurrencyLayer
{
    public class BaseRatesResponse
    {
        public bool Success { get; set; }
        public Uri Terms { get; set; }
        public Uri Privacy { get; set; }
        public long Timestamp { get; set; }
        public string Source { get; set; }
        public CurrencyLayerErrorResponse Error { get; set; }
    }
}
