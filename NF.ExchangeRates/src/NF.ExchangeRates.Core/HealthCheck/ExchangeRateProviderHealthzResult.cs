namespace NF.ExchangeRates.Core.HealthCheck
{
    public class ExchangeRateProviderHealthzResult
    {
        public bool ServiceHealthy { get; set; }
        public string Hostname { get; set; }
        public string Version { get; set; }
        public string Environment { get; set; }
        public string DeployDatetime { get; set; }
    }
}
