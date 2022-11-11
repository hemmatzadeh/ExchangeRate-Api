namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IUserExchangeRepository
    {
        Task<decimal> SaveExchangeAsync(int userId, string from, string to,decimal amount, decimal rate, CancellationToken cancellationToken);
        Task<int> GetCountAsync(int userId, DateTime fromdate, CancellationToken cancellationToken);
    }
}
