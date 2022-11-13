namespace NF.ExchangeRates.Core.Configuration
{
    public class ExchangeSettings
    {
        public const string SectionName = "ExchangeSettings";
        public int ValidMinutesForEachCachedRate { get; set; }
        public int ClientRequestLimitPerHour { get; set; }
    }
}