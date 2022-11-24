using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Interfaces;

namespace NF.ExchangeRates.ApiLayerCurrencyData
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApiLayerCurrency(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ApiLayerCurrencyDataOptions>(configuration.GetSection(ApiLayerCurrencyDataOptions.Section));


            services.AddTransient<ApiLayerCurrencyExchangeRateRetriever>();
          

            return services;
        }
    }
}
