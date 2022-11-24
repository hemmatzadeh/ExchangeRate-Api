using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.ExchangeRateHostCurrencyData
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddExchangeRateHostCurrency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ExchangeRateHostCurrencyDataOptions>(configuration.GetSection(ExchangeRateHostCurrencyDataOptions.Section));

            services.AddTransient<ExchangeRateHostCurrencyExchangeRateRetriever>();
            return services;
        }
    }
}
