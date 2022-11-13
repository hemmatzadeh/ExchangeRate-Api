namespace A5Labs.ExchangeRates.Tests
{
	using System;
	using System.Threading;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using Microsoft.Extensions.DependencyInjection.Extensions;
	using Microsoft.Extensions.Logging;
	using Moq;
	using NF.ExchangeRates.Core.Interfaces;
	using NF.ExchangeRates.Core;
	using Xunit;
	using NF.ExchangeRates.Core.Services;
	using Microsoft.Extensions.Configuration;
	using System.Text;
	using MediatR;

	[Trait("Category", "Unit")]
	public class GetExchangeRateServiceTests
	{
		private readonly IServiceProvider _provider;

		private readonly Mock<IExchangeRateRetriever> _retriever = new Mock<IExchangeRateRetriever>();
		private readonly Mock<IExchangeRateReader> _reader = new Mock<IExchangeRateReader>();
		private readonly Mock<IExchangeRateWriter> _writer = new Mock<IExchangeRateWriter>();
		private readonly Mock<ILogger<GetExchangeRateService>> _logger = new Mock<ILogger<GetExchangeRateService>>();

		public GetExchangeRateServiceTests()
		{
			var json = "{\"ExchangeSettings\": {\r\n    \"ValidMinutesForEachCachedRate\": 30,\r\n    \"ClientRequestLimitPerHour\": 10\r\n  } ,\r\n\r\n  \"HealthCheckPublisher\": {\r\n    \"DelaySeconds\": 10,\r\n    \"PeriodSeconds\": 30,\r\n    \"StatusChangeTolerance\": 3\r\n  }}";
			var config = new ConfigurationBuilder().AddJsonStream(new MemoryStream(Encoding.UTF8.GetBytes(json)))
				.Build();

			_provider = new ServiceCollection()
				.AddCore()
				.AddMemoryCache()
				.AddCoreSettings(config)
				.AddMediatR(typeof(Clock))
				.Replace(new ServiceDescriptor(typeof(ILogger<GetExchangeRateService>), _logger.Object))
				.Replace(new ServiceDescriptor(typeof(IExchangeRateReader), _reader.Object))
				.Replace(new ServiceDescriptor(typeof(IExchangeRateWriter), _writer.Object))
				.AddScoped((_) => _retriever.Object)
				.BuildServiceProvider();
		}

		[Fact]
		public async Task ShouldGetFromDatabaseIfExistInDatabase()
		{
			using var scope = _provider.CreateScope();
			var dt = DateTime.UtcNow;
			_reader.Setup(e => e.Read("USD", "CNY", It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ExchangeRate() { BaseCurrency = "USD", ToCurrency = "CNY", Rate = 1.23456M, Created = dt });

			var getExchange = scope.ServiceProvider.GetRequiredService<IGetExchangeRate>();
			var data = await getExchange.Execute("USD", "CNY", CancellationToken.None);

			data.Should().BeEquivalentTo(new ExchangeRate() { BaseCurrency = "USD", ToCurrency = "CNY", Rate = 1.23456M, Created = dt, Message = "Read rate from database" });
		}

		[Fact]
		public async Task ShouldGetFromRetrieverIfNotExistInDatabase()
		{
			using var scope = _provider.CreateScope();
			var dt = DateTime.UtcNow;
			_reader.Setup(e => e.Read("USD", "CNY", It.IsAny<CancellationToken>()))
				.ReturnsAsync((ExchangeRate)null);

			_retriever.Setup(r => r.GetExchangeRate("USD", "CNY", It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ExchangeRate() { BaseCurrency = "USD", ToCurrency = "CNY", Rate = 1.23456M, Created = dt });

			_writer.Setup(e => e.Write("USD", "CNY", 1.23456M, It.IsAny<CancellationToken>()))
				.Verifiable();
			var getExchange = scope.ServiceProvider.GetRequiredService<IGetExchangeRate>();

			var data = await getExchange.Execute("USD", "CNY", CancellationToken.None);

			data.Should().BeEquivalentTo(new ExchangeRate() { BaseCurrency = "USD", ToCurrency = "CNY", Rate = 1.23456M, Created = dt, Message = "Read rate from online provider" });
		}

		[Fact]
		public async Task ShouldThrowArgumentNullExceptionWhenFromParameterIsEmpty()
		{
			using var scope = _provider.CreateScope();

			_reader.Setup(e => e.Read(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((ExchangeRate)null);
			var dt = DateTime.UtcNow;
			_retriever.Setup(r => r.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ExchangeRate() { BaseCurrency = "CNY", Rate = 1.23456M, Created = dt });

			_writer.Setup(e => e.Write(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
				.Verifiable();

			var getExchange = scope.ServiceProvider.GetRequiredService<IGetExchangeRate>();
			Func<Task> action = async () => await getExchange.Execute("", "CNY", CancellationToken.None);

			await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task ShouldThrowArgumentNullExceptionWhenToParameterIsEmptyAsync()
		{
			using var scope = _provider.CreateScope();

			_reader.Setup(e => e.Read(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync((ExchangeRate)null);
			var dt = DateTime.UtcNow;
			_retriever.Setup(r => r.GetExchangeRate(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
				.ReturnsAsync(new ExchangeRate() { BaseCurrency = "CNY", Rate = 1.23456M, Created = dt });

			_writer.Setup(e => e.Write(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<CancellationToken>()))
				.Verifiable();

			var getExchange = scope.ServiceProvider.GetRequiredService<IGetExchangeRate>();
			Func<Task> action = async () => await getExchange.Execute("USD", "", CancellationToken.None);

			_ = await action.Should().ThrowAsync<ArgumentNullException>();
		}

		[Fact]
		public async Task ShouldReturnRate1WhenToEqualsFrom()
		{
			var dt = DateTime.UtcNow;
			using var scope = _provider.CreateScope();

			var getExchange = scope.ServiceProvider.GetRequiredService<IGetExchangeRate>();
			var data = await getExchange.Execute("USD", "USD", CancellationToken.None);

			data.Should().BeEquivalentTo(new ExchangeRate() { BaseCurrency = "USD", ToCurrency="USD", Rate = 1M, Created = dt.Date });
		}
	}
}
