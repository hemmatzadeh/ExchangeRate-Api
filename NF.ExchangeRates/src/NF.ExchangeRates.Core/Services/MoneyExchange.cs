using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NF.ExchangeRates.Core.Configuration;
using NF.ExchangeRates.Core.Exceptions;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Core.Services
{
    public class MoneyExchangeService : IMoneyExchangeService
    {
        private readonly ILogger<MoneyExchangeService> _logger;
        private readonly IClock _clock;
        private readonly ExchangeSettings _options;
        private readonly IMoneyExchangeReader _exchangeReader;
        private readonly IMoneyExchangeWriter _writer;
        public MoneyExchangeService(ILogger<MoneyExchangeService> logger, IClock clock,
            IOptions<ExchangeSettings> options, IMoneyExchangeReader exchangeReader, IMoneyExchangeWriter writer)
        {
            _logger = logger;
            _clock = clock;
            _options = options.Value;
            _exchangeReader = exchangeReader;
            _writer = writer;
        }

        public async Task<ExchangeResult> Execute(int userId, string from, string to, decimal amount, decimal rate, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Attempting to make money exchange userId:{userId}, from:{from}, to:{to}, amount:{amount}");

            var oneHourAgo = _clock.UtcNow.AddHours(-1);
            var requestLimit = _options.ClientRequestLimitPerHour;

            var exchangeCount = await _exchangeReader.GetUserExchangesCount(userId, oneHourAgo, cancellationToken);
            if (exchangeCount >= requestLimit)
            {
                _logger.LogError($"userId:{userId} exeeded request limit");
                throw new RequestLimitExceededException("Exceeded request limit");
            }

            var converted = await _writer.Execute(userId, from, to, amount, rate, cancellationToken);

            return new ExchangeResult { Amount = amount, ConvertedAmount = converted, Rate = rate };
        }
    }
}
