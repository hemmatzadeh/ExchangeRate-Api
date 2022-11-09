namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IGetExchangeRate
    {
        Task<ExchangeRate> Execute(int userId, string from, string to, CancellationToken cancellationToken = default);
    }
}
