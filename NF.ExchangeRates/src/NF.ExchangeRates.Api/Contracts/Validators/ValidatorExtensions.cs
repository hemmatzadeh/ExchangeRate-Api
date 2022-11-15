using FluentValidation;
using FluentValidation.Internal;
using FluentValidation.Results;
using System.Linq.Expressions;
using System.Reflection;

namespace NF.ExchangeRates.Api.Contracts.Validators
{
	public class Validation
	{
		public static string ResolvePropertyName(Type type, MemberInfo memberInfo, LambdaExpression expression)
		{
			return DefaultPropertyNameResolver(type, memberInfo, expression);
		}

		private static string DefaultPropertyNameResolver(Type type, MemberInfo memberInfo, LambdaExpression expression)
		{
			if (expression != null)
			{
				PropertyChain propertyChain = PropertyChain.FromExpression(expression);
				if (propertyChain.Count > 0)
				{
					return propertyChain.ToString();
				}
			}

			if (memberInfo != null)
			{
				return memberInfo.Name;
			}

			return null;
		}

		public static void Validate<T>(T value, AbstractValidator<T> validator)
		{
			ValidationResult validationResult = validator.Validate(value);
			if (!validationResult.IsValid)
			{
				throw new ValidationException(validationResult.Errors);
			}
		}
	}
}
