using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NF.ExchangeRates.Core.Configuration;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core.Services
{
    public class GetExchangeRateService : IGetExchangeRate
    {
        private readonly IExchangeRateRetriever _retriever;
        private readonly ILogger<GetExchangeRateService> _logger;
        private readonly IExchangeRateReader _reader;
        private readonly ExchangeSettings _options;
        private readonly IClock _clock;
        public GetExchangeRateService(IExchangeRateRetriever retriever, ILogger<GetExchangeRateService> logger, IExchangeRateReader reader, IOptions<ExchangeSettings> options, IClock clock)
        {
            _retriever = retriever;
            _logger = logger;
            _reader = reader;
            _options = options.Value;
            _clock = clock;
        }

        public Task<ExchangeRate> Execute(string from, string to, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(from))
            {
                throw new ArgumentNullException(nameof(from));
            }

            from = from.Trim().ToUpperInvariant();

            if (string.IsNullOrEmpty(to))
            {
                throw new ArgumentNullException(nameof(to));
            }

            to = to.Trim().ToUpperInvariant();

            return GetFromDbOrProvider(from, to, cancellationToken);
        }

        private async Task<ExchangeRate> GetFromDbOrProvider(string from, string to, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation("Attempting to get rates from cache");
            try
            {
                var rate = await _reader.Read(from, to, cancellationToken);
                if (rate != null)
                {
                    if (rate.Created > _clock.UtcNow.AddMinutes(-1 * _options.ValidMinutesForEachCachedRate))
                        return rate;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Read from database Error: {0}", ex);
            }

            _logger.LogInformation("Valid Rate {FromCurrency} -> {ToCurrency} not found in datastore. Retrieving from external provider", from, to);

            var providerRate = (await _retriever.GetExchangeRate(from, to, cancellationToken));

            return providerRate;
        }
    }
}
