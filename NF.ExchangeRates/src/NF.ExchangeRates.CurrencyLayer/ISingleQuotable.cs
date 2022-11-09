namespace NF.ExchangeRates.CurrencyLayer
{
    public interface ISingleQuotable
    {
        long Timestamp { get; set; }

        CurrencyLayerErrorResponse Error { get; set; }

        Dictionary<string, decimal> Quotes { get; set; }
    }
}
