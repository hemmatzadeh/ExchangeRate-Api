using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.MsSql.Services
{
    internal class MsSqlMoneyExchangeReader : IMoneyExchangeReader
    {
        private readonly IUserExchangeRepository _userExchangeRepository;
        public MsSqlMoneyExchangeReader(IUserExchangeRepository userExchangeRepository)
        {
            _userExchangeRepository = userExchangeRepository;
        }

        public async Task<int> GetUserExchangesCount(int userId, DateTime fromDate, CancellationToken cancellationToken)
        => await  _userExchangeRepository.GetCountAsync(userId, fromDate, cancellationToken);            

        
    }
}
