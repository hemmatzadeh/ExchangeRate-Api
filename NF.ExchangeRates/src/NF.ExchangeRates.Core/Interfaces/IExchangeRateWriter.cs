namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IExchangeRateWriter
    {
        Task Write(
            string from,
            string to,
            decimal rate,
            CancellationToken cancellationToken = default);

        Task WriteAll(string from, Dictionary<string, decimal> quotes, CancellationToken cancellationToken = default);
    }
}
