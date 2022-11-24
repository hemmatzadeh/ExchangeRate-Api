using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NF.ExchangeRates.Core;
using NF.ExchangeRates.Core.Enums;
using NF.ExchangeRates.MsSql.Persistence;
using NF.ExchangeRates.MsSql.Repositories;

namespace NF.ExchangeRates.Tests
{
    public class RepositoryTests
    {
        private readonly ExchangeContext _context;        
        public RepositoryTests()
        {
            var _contextOptions = new DbContextOptionsBuilder<ExchangeContext>()
                .UseInMemoryDatabase("ExchangeTest")
                .ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                .Options;

            _context = new ExchangeContext(_contextOptions);

        }

        #region RateRepositoryTests
        [Fact]
        public async Task GetAsyncShouldReturnNullForEmptyDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();
            var rep = new RateRepository(_context, clock);

            var data = await rep.GetAsync((short)ApiProviders.ApiLayer,"USD", "TRY", CancellationToken.None);

            data.Should().BeNull();
        }


        [Fact]
        public async Task SaveRateAsyncShouldAddOneRecordToDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();
            var rep = new RateRepository(_context, clock);

            await rep.SaveRateAsync((short)ApiProviders.ApiLayer,"USD", "TRY", 18.50M, CancellationToken.None);

            var data = await rep.GetAsync((short)ApiProviders.ApiLayer,"USD", "TRY", CancellationToken.None);

            data.Should().NotBeNull();
            data.BaseCurrency.Should().Be("USD");
            data.ToCurrency.Should().Be("TRY");
        }

        [Fact]
        public async Task SaveRatesAsyncShouldAddListOfRatesToDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();
            var rep = new RateRepository(_context, clock);

            var sentData = new Dictionary<string, decimal>();
            sentData.Add("USDTRY", 18.5M);
            sentData.Add("USDEUR", 1.01M);
            sentData.Add("USDIRR", 34010.5M);
            sentData.Add("USDCNY", 25.40M);

            await rep.SaveRatesAsync((short)ApiProviders.ApiLayer,"USD", sentData, CancellationToken.None);

            var count = _context.Rates.Count();
            count.Should().Be(4);
        }
        #endregion

        #region UserExchangeRepositoryTests

        [Fact]
        public async Task GetCountAsyncShouldReturnZeroForEmptyDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();

            var uxr = new UserExchangeRepository(_context, clock);
            var count = await uxr.GetCountAsync(100, clock.UtcNow.AddHours(-1), CancellationToken.None);

            count.Should().Be(0);
        }

        [Fact]
        public async Task SaveExchangeAsyncShouldAddRecord()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();

            var uxr = new UserExchangeRepository(_context, clock);
            var result = await uxr.SaveExchangeAsync(100, "USD", "TRY", 150, 18.55M, CancellationToken.None);
            result.Should().Be(150 * 18.55M);

            var count = await uxr.GetCountAsync(100,clock.UtcNow.AddMinutes(-2),CancellationToken.None);
            count.Should().Be(1);

        }
        #endregion

    }

}