using Microsoft.EntityFrameworkCore;
using NF.ExchangeRates.Core.Entities;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.MsSql.Persistence;

namespace NF.ExchangeRates.MsSql.Repositories
{
    public class RateRepository : IRateRepository
    {
        protected ExchangeContext _dbContext;
        private readonly IClock _clock;
        public RateRepository(ExchangeContext context, IClock clock)
        {
            _dbContext = context ?? throw new ArgumentNullException(nameof(context));
            _clock = clock ?? throw new ArgumentNullException();
        }

        public Task<RateInfo> GetAsync(string baseCurrency, string toCurrency, CancellationToken cancellationToken) => _dbContext.Rates
                .OrderByDescending(u => u.Id)
                .FirstOrDefaultAsync(u => u.BaseCurrency == baseCurrency && u.ToCurrency == toCurrency, cancellationToken);

        public async Task SaveRateAsync(string from, string to, decimal rate, CancellationToken cancellationToken)
        {
            await _dbContext.Rates.AddAsync(new RateInfo
            {
                BaseCurrency = from,
                ToCurrency = to,
                Created = _clock.UtcNow,
                Rate = rate
            }, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }


        public async Task SaveRatesAsync(string from, Dictionary<string, decimal> Quotes, CancellationToken cancellationToken)
        {
            var created = _clock.UtcNow;
            
            foreach (var rate in Quotes)
            {
                _dbContext.Rates.Add(new RateInfo
                {
                    BaseCurrency = from,
                    Rate = rate.Value,
                    Created = created,
                    ToCurrency = rate.Key.Substring(3)
                });
            }
            
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
