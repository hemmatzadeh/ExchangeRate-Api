using FluentValidation;
using HealthChecks.UI.Client;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using NF.ExchangeRates.Api.HealthChecks;
using NF.ExchangeRates.ApiLayerCurrencyData;
using NF.ExchangeRates.ExchangeRateHostCurrencyData;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.CurrencyLayer;
using NF.ExchangeRates.MsSql;
using MediatR;
using FluentValidation.AspNetCore;
using NF.ExchangeRates.Api.Contracts.Validators;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.Core.Interfaces;
using System.Text.Json.Serialization;

namespace NF.ExchangeRates.Api
{

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddControllers();
            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            builder.Services.AddTransient<IValidatorFactory, ServiceProviderValidatorFactory>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddFluentValidationRulesToSwagger();
            builder.Services.AddSwaggerGen();

            builder.Services
                //.AddCurrencyLayer(builder.Configuration)
                .AddApiLayerCurrency(builder.Configuration)
                .AddExchangeRateHostCurrency(builder.Configuration)
                .AddMsSql(builder.Configuration)
                .AddCore()
                .AddCoreSettings(builder.Configuration);

            builder.Services.AddTransient<Func<ApiProviders, IExchangeRateRetriever>>((serviceProvider => key =>
            {
                return key switch
                {
                    ApiProviders.ApiLayer => serviceProvider.GetService<ApiLayerCurrencyExchangeRateRetriever>(),
                    ApiProviders.ExchangeRate => serviceProvider.GetService<ExchangeRateHostCurrencyExchangeRateRetriever>(),
                    _ => throw new NotImplementedException(),
                };
            }));

            builder.Services
                .AddCurrencyHealthCheck(builder.Configuration)
                .AddHealthChecks()
                .AddCheck<ExchangeRateProviderHealthCheck>(name: "ExchangeRateProvider", failureStatus: HealthStatus.Unhealthy, timeout: TimeSpan.FromSeconds(15), tags: new[] { "ready" })
                .AddSqlServer(builder.Configuration.GetConnectionString("Default"), failureStatus: HealthStatus.Unhealthy, timeout: TimeSpan.FromSeconds(15), tags: new[] { "ready" });

            builder.Services.AddMediatR(typeof(Clock));
            builder.Services.AddMemoryCache();

            builder.Services.AddFluentValidationAutoValidation(options =>
            {
                ValidatorOptions.Global.PropertyNameResolver = Validation.ResolvePropertyName;
                ValidatorOptions.Global.DisplayNameResolver = Validation.ResolvePropertyName;
                options.DisableDataAnnotationsValidation = true;

            }).AddFluentValidationClientsideAdapters();

            builder.Services.AddValidatorsFromAssemblyContaining<ExchangeRequestValidator>();
            
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
