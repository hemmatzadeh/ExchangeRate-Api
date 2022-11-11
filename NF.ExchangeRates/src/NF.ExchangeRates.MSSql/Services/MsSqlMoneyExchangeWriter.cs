using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.MsSql.Services
{
    public class MsSqlMoneyExchangeWriter : IMoneyExchangeWriter
    {
        private readonly IUserExchangeRepository _userExchangeRepository;
        public MsSqlMoneyExchangeWriter(IUserExchangeRepository userExchangeRepository)
        {
            _userExchangeRepository = userExchangeRepository;
        }

        public async Task<decimal> Execute(int userId, string from, string to, decimal amount, decimal rate, CancellationToken cancellationToken)
        => await _userExchangeRepository.SaveExchangeAsync(userId, from, to, amount, rate, cancellationToken);
    }
}
