using Microsoft.AspNetCore.Mvc;
using NF.ExchangeRates.Api.Contracts;
using NF.ExchangeRates.Api.Contracts.Validators;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateController : ControllerBase
    {
        private readonly ILogger<RateController> _logger;
        private readonly IGetExchangeRate _query;
        public RateController(ILogger<RateController> logger, IGetExchangeRate query)
        {
            _logger = logger;
            _query = query;
        }

        [HttpGet("/api/Rate/{From}/{To}")]
        public async Task<IActionResult> Get([FromRoute] GetRateRequest request)
        {
            var validator = new GetRateRequestValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                _logger.LogError("invalid request: {@Request} \r\n {@validationResult }", request, validationResult);
                return BadRequest(validationResult);
            }
            _logger.LogInformation("Request to retrieve exchange rate received: {@Request}", request);
            var rate = await _query.Execute(request.UserId, request.From.ToUpperInvariant(), request.To.ToUpperInvariant());

            var response = new GetRateResponse
            {
                From = request.From.ToUpperInvariant(),
                Created = rate.Created,
                To = rate.ToCurrency.ToUpperInvariant(),
                Rate = rate.Rate
            };

            return Ok(response);
        }

        [HttpPost("/api/Exchange/{UserId}/{From}/{To}/{Amount}")]
        public async Task<IActionResult> Exchange([FromRoute] ExchangeRequest request)
        {
            return Ok();
        }
    }
}
