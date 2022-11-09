namespace NF.ExchangeRates.CurrencyLayer.HealthCheck
{

    public class CurrencyLayerHealthResponse
    {
        public bool Service_healthy { get; set; }
        public string Hostname { get; set; }
        public string Version { get; set; }
        public string Environment { get; set; }
        public string Deploy_datetime { get; set; }
    }
}
