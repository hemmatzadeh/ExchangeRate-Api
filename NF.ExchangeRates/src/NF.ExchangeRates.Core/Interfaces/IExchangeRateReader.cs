namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateReader
    {
        Task<ExchangeRate> Read(string from, string to, CancellationToken cancellationToken = default);
    }
}
