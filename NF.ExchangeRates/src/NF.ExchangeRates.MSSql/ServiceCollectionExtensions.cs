using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NF.ExchangeRates.Core.Interfaces;
using NF.ExchangeRates.MsSql.Persistence;
using NF.ExchangeRates.MsSql.Repositories;
using NF.ExchangeRates.MsSql.Services;

namespace NF.ExchangeRates.MsSql
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMsSql(this IServiceCollection services, IConfiguration configuration)
        {
            var cnn = configuration.GetConnectionString("Default");
            services.AddDbContext<ExchangeContext>(options =>
            {
                options.UseSqlServer(cnn);
            });

            services.AddScoped<IRateRepository, RateRepository>();
            services.AddScoped<IExchangeRateReader, MsSqlExchangeRateReader>();
            services.AddScoped<IExchangeRateWriter, MsSqlExchangeRateWriter>();

            return services;
        }
    }
}