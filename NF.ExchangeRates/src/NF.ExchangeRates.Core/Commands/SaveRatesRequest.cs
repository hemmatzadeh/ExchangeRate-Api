using MediatR;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core.Commands
{
    public class SaveRatesRequest : IRequest
    {
        public string BaseCurrency { get; set; }
        public Dictionary<string, decimal> Quotes { get; set; }
    }

    public class SaveRatesRequestHandler : IRequestHandler<SaveRatesRequest>
    {
        private readonly IExchangeRateWriter _writer;
        public SaveRatesRequestHandler(IExchangeRateWriter writer)
        {
            _writer= writer;
        }
        public async Task<Unit> Handle(SaveRatesRequest request, CancellationToken cancellationToken)
        {
            await _writer.WriteAll(request.BaseCurrency, request.Quotes , cancellationToken);
            return new Unit();
        }
    }
}
