namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IMoneyExchangeReader
    {
        Task<int> GetUserExchangesCount(int userId, DateTime fromDate, CancellationToken cancellationToken);
    }
}
