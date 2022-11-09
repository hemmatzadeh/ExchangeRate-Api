using MediatR;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.CurrencyLayer
{
    public class CurrencyLayerExchangeRateRetriever : IExchangeRateRetriever
    {
        private readonly IClock _clock;
        private readonly IRealTimeRatesService _realTimeRatesService;
        private readonly IMediator _mediator;
        public CurrencyLayerExchangeRateRetriever(IClock clock, IRealTimeRatesService realTimeRatesService,IMediator mediator)
        {
            _clock = clock;
            _realTimeRatesService = realTimeRatesService;
            _mediator = mediator;
        }

        public async Task<ExchangeRate> GetExchangeRate(string from, string to, CancellationToken cancellationToken)
        {
            ISingleQuotable response = await _realTimeRatesService.GetRealTimeRates(from, to);

            return new ExchangeRate
            {
                BaseCurrency = from,
                ToCurrency= to,
                Rate = response.Quotes[$"{from}{to}"], 
                Created= _clock.UtcNow,
            };
        }
    }
}
