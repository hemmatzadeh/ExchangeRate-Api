namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IMoneyExchangeWriter
    {
        Task<decimal> Execute(int userId, string from, string to, decimal amount, decimal rate, CancellationToken cancellationToken);
    }
}
