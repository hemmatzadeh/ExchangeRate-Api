using Refit;

namespace NF.ExchangeRates.CurrencyLayer.HealthCheck
{
    public interface ICurrencyLayerHealthReader
    {
        [Get("/healthz")]
        Task<CurrencyLayerHealthResponse> GetHealthz();
    }
}
