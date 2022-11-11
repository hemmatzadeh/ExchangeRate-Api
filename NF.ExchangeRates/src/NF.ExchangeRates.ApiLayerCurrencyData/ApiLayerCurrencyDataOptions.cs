namespace NF.ExchangeRates.ApiLayerCurrencyData
{
    public class ApiLayerCurrencyDataOptions
    {
        public const string Section = "ApiLayerCurrencyData";

        public string ApiKey { get; set; }
        public Uri BaseAddress { get; set; }
    }
}