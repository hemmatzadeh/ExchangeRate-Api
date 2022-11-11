using Refit;
namespace NF.ExchangeRates.CurrencyLayer
{
    public interface IRealTimeRatesService
    {
        [Get("/live")]
        Task<RealTimeRatesResponse> GetRealTimeRates([Query] string source,
            [Query(CollectionFormat.Csv)] params string[] currencies);
    }
}