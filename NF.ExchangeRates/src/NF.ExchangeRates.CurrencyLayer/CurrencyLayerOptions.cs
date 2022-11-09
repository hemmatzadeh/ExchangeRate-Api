namespace NF.ExchangeRates.CurrencyLayer
{
    public class CurrencyLayerOptions
    {
        public const string Section = "CurrencyLayer";

        public string AccessKey { get; set; }
        public Uri BaseAddress { get; set; }
    }
}
