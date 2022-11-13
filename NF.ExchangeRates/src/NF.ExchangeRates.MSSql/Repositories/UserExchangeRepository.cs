using Microsoft.EntityFrameworkCore;
using NF.ExchangeRates.Core.Entities;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.MsSql.Persistence;

namespace NF.ExchangeRates.MsSql.Repositories
{
    public class UserExchangeRepository : IUserExchangeRepository
    {
        protected ExchangeContext _dbContext;
        private readonly IClock _clock;
        public UserExchangeRepository(ExchangeContext dbContext,IClock clock)
        {
            _dbContext = dbContext;
            _clock = clock;
        }

        public async Task<int> GetCountAsync(int userId, DateTime fromdate, CancellationToken cancellationToken)
        {
            return await _dbContext.userExchangeInfos.Where(u=>u.Created>=fromdate).CountAsync();
        }

        public async Task<decimal> SaveExchangeAsync(int userId, string from, string to, decimal amount, decimal rate, CancellationToken cancellationToken)
        {
            var model = new UserExchangeInfo
            {
                UserId = userId,
                Amount = amount,
                Created = _clock.UtcNow,
                BaseCurrency = from,
                ExchangeRate = rate,
                ToCurrency = to
            };

            //await _dbContext.Database.ExecuteSqlRawAsync(buildSqlCmd(model), cancellationToken);
            _dbContext.userExchangeInfos.Add(model);
            await _dbContext.SaveChangesAsync();
            return model.Amount* model.ExchangeRate;
        }

        private string buildSqlCmd(UserExchangeInfo model) =>
            $"exec AddUserTrade {model.UserId},'{model.BaseCurrency}','{model.ToCurrency}',{model.Amount},{model.ExchangeRate}";

    }
}
