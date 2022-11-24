using Flurl;
using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Commands;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Exceptions;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.ExchangeRateHostCurrencyData;

namespace NF.ExchangeRates.ExchangeRateHostCurrencyData
{
    public class ExchangeRateHostCurrencyExchangeRateRetriever : IExchangeRateRetriever
    {
        private readonly IClock _clock;
        private readonly IOptions<ExchangeRateHostCurrencyDataOptions> _options;
        private readonly ILogger<ExchangeRateHostCurrencyExchangeRateRetriever> _logger;
        private readonly IMediator _mediator;
        public ExchangeRateHostCurrencyExchangeRateRetriever(IClock clock, IOptions<ExchangeRateHostCurrencyDataOptions> options, ILogger<ExchangeRateHostCurrencyExchangeRateRetriever> logger, IMediator mediator)
        {
            _clock = clock;
            _options = options;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ExchangeRate> GetExchangeRate(ApiProviders provider,string from, string to,  CancellationToken cancellationToken)
        {
            var apiConfig = _options.Value;
            var response = await apiConfig.BaseAddress
                .SetQueryParams(new { from, to })
                .GetAsync();

            if (response.StatusCode != 200)
            {
                _logger.LogError($"{response.StatusCode}:{response.ResponseMessage}");
                throw new Exception($"{response.StatusCode}:{response.ResponseMessage}");
            }
            var txtdata = await response.GetStringAsync();
            var rateData = JsonConvert.DeserializeObject<LiveRatesResponse>(txtdata);

            try
            {
                await _mediator.Send(new SaveRatesRequest { Provider = provider, BaseCurrency = from, Quotes = rateData.Quotes }, cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in saving Rates {ex.Message} ");
            }

            if (!rateData.Success)
                return null;
            var key = $"{from}{to}";

            if (!rateData.Quotes.Keys.Contains(key))
                throw new RateNotFoundException($"Data not found for exchange {from} to {to} in [{provider}]!");

            return new ExchangeRate
            {
                Provider = provider,
                BaseCurrency = from,
                ToCurrency = to,
                Created = _clock.UtcNow,
                Rate = rateData.Quotes[key]
            };
        }
    }
}
