using MediatR;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core.Commands
{
    public class SaveRateRequest : IRequest
    {
        public ApiProviders Provider { get; set; }
        public string BaseCurrency { get; set; }
        public string ToCurrency { get; set; }
        public decimal Rate { get; set; }

    }
    public class SaveRateRequestHandler : IRequestHandler<SaveRateRequest>
    {
        private readonly IExchangeRateWriter _writer;
        public SaveRateRequestHandler(IExchangeRateWriter writer)
        {
            _writer = writer;
        }
        public async Task<Unit> Handle(SaveRateRequest request, CancellationToken cancellationToken)
        {
            await _writer.Write(request.Provider, request.BaseCurrency, request.ToCurrency, request.Rate, cancellationToken);
            return new Unit();
        }
    }
}
