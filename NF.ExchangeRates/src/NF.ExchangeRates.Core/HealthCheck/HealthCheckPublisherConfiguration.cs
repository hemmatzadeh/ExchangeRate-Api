namespace NF.ExchangeRates.Core.HealthCheck
{
    public class HealthCheckPublisherConfiguration
    {
        public const string Section = "HealthCheckPublisher";
        public int DelaySeconds { get; set; } = 30;
        public int PeriodSeconds { get; set; } = 60;
        public int StatusChangeTolerance { get; set; } = 3;
    }
}
