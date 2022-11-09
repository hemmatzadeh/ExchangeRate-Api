using Microsoft.Extensions.Logging;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core.Services
{
    public class GetExchangeRate : IGetExchangeRate
    {
        private readonly IExchangeRateRetriever _retriever;
        private readonly ILogger<GetExchangeRate> _logger;
        private readonly IExchangeRateReader _reader;
        private readonly IExchangeRateWriter _writer;

        public GetExchangeRate(IExchangeRateRetriever retriever, ILogger<GetExchangeRate> logger, IExchangeRateReader reader, IExchangeRateWriter writer)
        {
            _retriever = retriever;
            _logger = logger;
            _reader = reader;
            _writer = writer;
        }

        public Task<ExchangeRate> Execute(int userId, string from, string to, CancellationToken cancellationToken = default)
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
                    return rate;
                }
            }catch(Exception ex)
            {
                _logger.LogError("Read from database Error: {0}", ex);
            }

            _logger.LogInformation("Valid Rate {FromCurrency} -> {ToCurrency} not found in datastore. Retrieving from external provider", from, to);

            var providerRate = (await _retriever.GetExchangeRate(from, to, cancellationToken));

            await _writer.Write(from, to, providerRate.Rate, cancellationToken);

            return providerRate;
        }
    }
}
