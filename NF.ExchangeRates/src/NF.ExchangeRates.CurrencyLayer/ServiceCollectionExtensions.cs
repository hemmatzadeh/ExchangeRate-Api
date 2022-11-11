using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.CurrencyLayer.HealthCheck;
using Refit;

namespace NF.ExchangeRates.CurrencyLayer
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCurrencyLayer(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencyLayerOptions>(configuration.GetSection(CurrencyLayerOptions.Section));

            services.AddRefitClient<IRealTimeRatesService>()
                .ConfigureHttpClient((provider, client) =>
                {
                    var options = provider.GetRequiredService<IOptions<CurrencyLayerOptions>>();
                    client.BaseAddress = options.Value.BaseAddress;
                })
                .AddHttpMessageHandler<CurrencyLayerAuthorizationHandler>();


            services.AddTransient<CurrencyLayerAuthorizationHandler>();
            services.AddTransient<IExchangeRateRetriever, CurrencyLayerExchangeRateRetriever>();
            

            return services;
        }

        public static IServiceCollection AddCurrencyHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CurrencyLayerOptions>(configuration.GetSection(CurrencyLayerOptions.Section));

            services.AddRefitClient<ICurrencyLayerHealthReader>()
            .ConfigureHttpClient((provider, client) =>
            {
                var options = provider.GetRequiredService<IOptions<CurrencyLayerOptions>>();
                client.BaseAddress = options.Value.BaseAddress;
            });

            services.AddTransient<IExchangeRateProviderHealthReader, CurrencyLayerHealthReader>();
            return services;
        }
    }
}
