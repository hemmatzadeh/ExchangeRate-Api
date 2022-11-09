namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateRetriever
    {
        Task<ExchangeRate> GetExchangeRate(string from, string to, CancellationToken cancellationToken);

    }
}
