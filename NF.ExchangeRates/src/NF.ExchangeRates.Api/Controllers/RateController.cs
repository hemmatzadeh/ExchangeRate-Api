using Microsoft.AspNetCore.Mvc;
using NF.ExchangeRates.Api.Contracts;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> _logger;
        private readonly IGetExchangeRate _query;
        private readonly IMoneyExchangeService _exchange;
        public RateController(ILogger<RateController> logger, IGetExchangeRate query, IMoneyExchangeService exchange)
        {
            _logger = logger;
            _query = query;
            _exchange = exchange;
        }

        [HttpGet("/api/Rate/{From}/{To}")]
        public async Task<IActionResult> Get([FromRoute] GetRateRequest request)
        {
            _logger.LogInformation("Request to retrieve exchange rate received: {@Request}", request);
            var rate = await _query.Execute(request.From.ToUpperInvariant(), request.To.ToUpperInvariant());

            var response = new GetRateResponse(rate.BaseCurrency, rate.ToCurrency, rate.Rate, rate.Created);

            return Ok(new { result = response, rate.Message });
        }

        [HttpPost("/api/Exchange/{UserId}/{From}/{To}/{Amount}")]
        public async Task<IActionResult> Exchange([FromRoute] ExchangeRequest request)
        {
            _logger.LogInformation("Request to money exchange received: {@Request}", request);
            
            var rate = await _query.Execute(request.From.ToUpperInvariant(), request.To.ToUpperInvariant());
            _logger.LogInformation($"Read exchange rate {request.From}-{request.To}:{rate.Rate}");
            
            var result =await _exchange.Execute(request.UserId, rate.BaseCurrency, rate.ToCurrency, request.Amount, rate.Rate);

            return Ok(new { result, rate.Message });
        }
    }
}
