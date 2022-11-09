namespace NF.ExchangeRates.ApiLayerCurrencyData
{
    public interface ISingleQuotable
    {
        long Timestamp { get; set; }

        ApiLayerErrorResponse Error { get; set; }

        Dictionary<string, decimal> Quotes { get; set; }
    }
    public class LiveRatesResponse : BaseRatesResponse, ISingleQuotable
    {
        public Dictionary<string, decimal> Quotes { get; set; }
    }
}
