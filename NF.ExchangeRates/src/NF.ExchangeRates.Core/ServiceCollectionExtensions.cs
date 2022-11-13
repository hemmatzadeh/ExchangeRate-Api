using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NF.ExchangeRates.Core.Configuration;
using NF.ExchangeRates.Core.HealthCheck;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.Core.Services;

namespace NF.ExchangeRates.Core
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddSingleton<IClock, Clock>();
            services.AddScoped<IGetExchangeRate, GetExchangeRateService>();
            services.AddScoped<IMoneyExchangeService, MoneyExchangeService>();
            
            return services;
        }
		public static IServiceCollection AddCoreSettings(this IServiceCollection services, IConfiguration configuration)
		{
			services.Configure<ExchangeSettings>(configuration.GetSection(ExchangeSettings.SectionName));

			var config = configuration.GetSection(HealthCheckPublisherConfiguration.Section)
				.Get<HealthCheckPublisherConfiguration>();

			config ??= new HealthCheckPublisherConfiguration();
			services.Configure<HealthCheckPublisherOptions>(options =>
			{
				options.Delay = TimeSpan.FromSeconds(config.DelaySeconds);
				options.Period = TimeSpan.FromSeconds(config.PeriodSeconds);
				options.Predicate = (check) => check.Tags.Contains("ready");
			});

			services.AddSingleton<IHealthCheckPublisher, HealthStatusPublisher>();

			return services;
		}
	}
}
