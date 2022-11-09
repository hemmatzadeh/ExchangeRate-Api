namespace NF.ExchangeRates.CurrencyLayer
{
    public class RealTimeRatesResponse : BaseRatesResponse, ISingleQuotable
    {
        public Dictionary<string, decimal> Quotes { get; set; }
    }
}
