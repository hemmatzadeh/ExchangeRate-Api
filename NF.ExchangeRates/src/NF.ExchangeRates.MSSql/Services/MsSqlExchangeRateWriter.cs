using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.MsSql.Services
{
    public class MsSqlExchangeRateWriter : IExchangeRateWriter
    {
        private readonly IRateRepository _rateRepository;
        public MsSqlExchangeRateWriter(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task Write(ApiProviders provider, string from, string to, decimal rate, CancellationToken cancellationToken = default)
        {
            await _rateRepository.SaveRateAsync((short)provider,from, to, rate, cancellationToken);
        }

        public async Task WriteAll(ApiProviders provider, string from, Dictionary<string, decimal> quotes, CancellationToken cancellationToken = default)
        {
            await _rateRepository.SaveRatesAsync((short)provider,from, quotes, cancellationToken);
        }
    }
}
