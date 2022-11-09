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

        public async Task Write(string from, string to, decimal rate, CancellationToken cancellationToken = default)
        {
            await _rateRepository.SaveRateAsync(from, to, rate, cancellationToken);
        }

        public async Task WriteAll(string from, Dictionary<string, decimal> quotes, CancellationToken cancellationToken = default)
        {
            await _rateRepository.SaveRatesAsync(from, quotes, cancellationToken);
        }
    }
}
