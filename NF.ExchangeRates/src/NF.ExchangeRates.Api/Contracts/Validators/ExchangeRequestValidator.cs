using FluentValidation;

namespace NF.ExchangeRates.Api.Contracts.Validators
{
    public class ExchangeRequestValidator : AbstractValidator<ExchangeRequest>
    {
        public ExchangeRequestValidator()
        {
            RuleFor(req => req.UserId).GreaterThan(0);
            RuleFor(req => req.From).NotEmpty().Length(3);
            RuleFor(req => req.To).NotEmpty().Length(3);
            RuleFor(req => req.Amount).GreaterThan(1);
        }
    }
}
