namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IGetExchangeRate
    {
        Task<ExchangeRate> Execute(string from, string to, CancellationToken cancellationToken = default);
    }
}
