namespace NF.ExchangeRates.ApiLayerCurrencyData
{
    public class BaseRatesResponse
    {
        public bool Success { get; set; }
        public long Timestamp { get; set; }
        public string Source { get; set; }
        public ApiLayerErrorResponse Error { get; set; }
    }
}
