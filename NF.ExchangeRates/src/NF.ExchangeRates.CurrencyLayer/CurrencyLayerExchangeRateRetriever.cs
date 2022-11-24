using MediatR;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Commands;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.CurrencyLayer
{
    public class CurrencyLayerExchangeRateRetriever : IExchangeRateRetriever
    {
        private readonly IClock _clock;
        private readonly IRealTimeRatesService _realTimeRatesService;
        private readonly IMediator _mediator;
        public CurrencyLayerExchangeRateRetriever(IClock clock, IRealTimeRatesService realTimeRatesService, IMediator mediator)
        {
            _clock = clock;
            _realTimeRatesService = realTimeRatesService;
            _mediator = mediator;
        }

        public async Task<ExchangeRate> GetExchangeRate(ApiProviders provider,string from, string to,  CancellationToken cancellationToken)
        {
            ISingleQuotable response = await _realTimeRatesService.GetRealTimeRates(from, to);

            var rate = response.Quotes[$"{from}{to}"];

            await _mediator.Send(new SaveRateRequest() { Provider = provider, BaseCurrency = from, ToCurrency = to, Rate = rate }, cancellationToken);

            return new ExchangeRate
            {
                Provider = provider,
                BaseCurrency = from,
                ToCurrency = to,
                Rate = rate,
                Created = _clock.UtcNow,
            };
        }
    }
}
