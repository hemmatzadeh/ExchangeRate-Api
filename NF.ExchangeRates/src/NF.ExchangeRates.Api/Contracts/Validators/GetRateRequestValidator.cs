using FluentValidation;

namespace NF.ExchangeRates.Api.Contracts.Validators
{
    public class GetRateRequestValidator : AbstractValidator<GetRateRequest>
    {
        public GetRateRequestValidator()
        {
            RuleFor(req => req.From).NotEmpty().Length(3).WithMessage("from parameter should be 3 characters, for example usd");
            RuleFor(req => req.To).NotEmpty().Length(3).WithMessage("to parameter should be 3 characters, for example try");
        }
    }

}
