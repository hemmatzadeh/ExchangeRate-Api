using Microsoft.Extensions.Caching.Memory;
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
		private readonly IMemoryCache _cache;
		public GetExchangeRateService(IExchangeRateRetriever retriever, ILogger<GetExchangeRateService> logger,
			IExchangeRateReader reader, IOptions<ExchangeSettings> options, IClock clock, IMemoryCache cache)
		{
			_retriever = retriever;
			_logger = logger;
			_reader = reader;
			_options = options.Value;
			_clock = clock;
			_cache = cache;
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
			if (from.Equals(to, StringComparison.OrdinalIgnoreCase))
			{
				return Task.FromResult(new ExchangeRate { BaseCurrency = from, Rate = 1M, Created = _clock.UtcNow.Date, ToCurrency = to });
			}
			return GetFromDbOrProvider(from, to, cancellationToken);
		}

		private async Task<ExchangeRate> GetFromDbOrProvider(string from, string to, CancellationToken cancellationToken = default)
		{
			_logger.LogInformation("Attempting to get rates from cache");
			var key = $"{from}_{to}";
			try
			{
				var cahed_rate = _cache.Get<ExchangeRate>(key);
				if (cahed_rate != null)
				{
					if (cahed_rate.Created > _clock.UtcNow.AddMinutes(-1 * _options.ValidMinutesForEachCachedRate))
					{
						cahed_rate.Message = "Read Rate from Cache";
						return cahed_rate;
					}
				}
				var rate = await _reader.Read(from, to, cancellationToken);
				if (rate != null)
				{
					if (rate.Created > _clock.UtcNow.AddMinutes(-1 * _options.ValidMinutesForEachCachedRate))
					{
						_cache.Remove(key);
						_cache.Set(key, rate);
						rate.Message = "Read rate from database";
						return rate;
					}
				}
			}
			catch (Exception ex)
			{
				_logger.LogError("Read from database Error: {0}", ex);
			}

			_logger.LogInformation("Valid Rate {FromCurrency} -> {ToCurrency} not found in datastore. Retrieving from external provider", from, to);

			var providerRate = (await _retriever.GetExchangeRate(from, to, cancellationToken));
			_cache.Set(key, providerRate);
			providerRate.Message = "Read rate from online provider";
			return providerRate;
		}


	}
}
