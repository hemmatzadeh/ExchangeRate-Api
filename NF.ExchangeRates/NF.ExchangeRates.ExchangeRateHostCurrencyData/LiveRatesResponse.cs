namespace NF.ExchangeRates.ExchangeRateHostCurrencyData
{
    public interface ISingleQuotable
    {
        DateTime Date { get; set; }

        ExchangeRateHostErrorResponse Error { get; set; }

        decimal Result { get; set; }
    }
    public class LiveRatesResponse : BaseRatesResponse, ISingleQuotable
    {
        public Dictionary<string, decimal> Quotes
        {
            get {
                return new Dictionary<string, decimal> { { $"{Query["from"]}{Query["to"]}", Info.First().Value } };
            }
        }
    }
}
