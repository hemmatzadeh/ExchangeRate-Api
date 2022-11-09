using FluentValidation;
using HealthChecks.UI.Client;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NF.ExchangeRates.Api.HealthChecks;
using NF.ExchangeRates.ApiLayerCurrencyData;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.CurrencyLayer;
using NF.ExchangeRates.MsSql;
using MediatR;

namespace NF.ExchangeRates.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddFluentValidationRulesToSwagger();
            builder.Services.AddSwaggerGen();

            builder.Services
                //.AddCurrencyLayer(builder.Configuration)
                .AddApiLayerCurrency(builder.Configuration)
                .AddMsSql(builder.Configuration)
                .AddCore(builder.Configuration);

            builder.Services
                .AddCurrencyHealthCheck(builder.Configuration)
                .AddHealthChecks()
                .AddCheck<ExchangeRateProviderHealthCheck>(name: "ExchangeRateProvider", failureStatus: HealthStatus.Unhealthy, timeout: TimeSpan.FromSeconds(15), tags: new[] { "ready" })
                .AddSqlServer(builder.Configuration.GetConnectionString("Default"), failureStatus: HealthStatus.Unhealthy, timeout: TimeSpan.FromSeconds(15), tags: new[] { "ready" });

            builder.Services.AddMediatR(typeof(Clock));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
            app.MapHealthChecks("/healthz", new HealthCheckOptions
            {
                AllowCachingResponses = true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.Run();
        }
    }
}