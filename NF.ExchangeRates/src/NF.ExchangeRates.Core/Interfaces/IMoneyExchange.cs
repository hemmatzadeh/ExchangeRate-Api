namespace NF.ExchangeRates.Core.Interfaces
{
    public interface IMoneyExchangeService
    {
        Task<ExchangeResult> Execute(int userId, string from, string to, decimal amount,decimal rate, CancellationToken cancellationToken = default);
    }
}
