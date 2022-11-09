using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.MsSql.Services
{
    public class MsSqlExchangeRateReader : IExchangeRateReader
    {
        private readonly IRateRepository _rateRepository;
        public MsSqlExchangeRateReader(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<ExchangeRate> Read(string from, string to, CancellationToken cancellationToken = default)
        {
            var data = await _rateRepository.GetAsync(from, to, cancellationToken);

            if (data == null)
                return null;

            return new ExchangeRate
            {
                BaseCurrency = data.BaseCurrency,
                Created = data.Created,
                Rate = data.Rate,
                ToCurrency = data.ToCurrency
            };
        }
    }
}
