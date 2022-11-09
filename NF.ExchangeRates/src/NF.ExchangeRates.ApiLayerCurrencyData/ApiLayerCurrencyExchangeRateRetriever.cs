using Flurl.Http;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Commands;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.ApiLayerCurrencyData
{
    public class ApiLayerCurrencyExchangeRateRetriever : IExchangeRateRetriever
    {
        private readonly IClock _clock;
        private readonly IOptions<ApiLayerCurrencyDataOptions> _options;
        private readonly ILogger<ApiLayerCurrencyExchangeRateRetriever> _logger;
        private readonly IMediator _mediator;
        public ApiLayerCurrencyExchangeRateRetriever(IClock clock, IOptions<ApiLayerCurrencyDataOptions> options, ILogger<ApiLayerCurrencyExchangeRateRetriever> logger,IMediator mediator)
        {
            _clock = clock;
            _options = options;
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<ExchangeRate> GetExchangeRate(string from, string to, CancellationToken cancellationToken)
        {
            var apiConfig = _options.Value;
            var response = await apiConfig.BaseAddress
                .WithHeader("apikey", apiConfig.ApiKey)
                .SetQueryParams(new { Base =from, symbols=to })
                .GetAsync();

            if (response.StatusCode != 200)
            {
                _logger.LogError($"{response.StatusCode}:{response.ResponseMessage}");
                throw new Exception($"{response.StatusCode}:{response.ResponseMessage}");
            }
            var txtdata = await response.GetStringAsync();
            var rateData = JsonConvert.DeserializeObject<LiveRatesResponse>(txtdata);

            await _mediator.Send(new SaveRatesRequest { BaseCurrency = from, Quotes = rateData.Quotes }, cancellationToken);

            if (!rateData.Success)
                return null;
            
            return new ExchangeRate
            {
                BaseCurrency = from,
                ToCurrency = to,
                Created = _clock.UtcNow,
                Rate = rateData.Quotes[$"{from}{to}"]
            };
        }
    }
}
