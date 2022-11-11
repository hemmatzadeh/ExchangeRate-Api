using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NF.ExchangeRates.Core;
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
        [Fact]
        public async Task GetAsyncShouldReturnNullForEmptyDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();
            var rep = new RateRepository(_context, clock);

            var data = await rep.GetAsync("USD", "TRY", CancellationToken.None);

            data.Should().BeNull();
        }


        [Fact]
        public async Task SaveRateAsyncShouldAddOneRecordToDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            var clock = new Clock();
            var rep = new RateRepository(_context, clock);

            await rep.SaveRateAsync("USD", "TRY", 18.50M, CancellationToken.None);

            var data =await rep.GetAsync("USD", "TRY", CancellationToken.None);

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

            await rep.SaveRatesAsync("USD", sentData, CancellationToken.None);

            var count = _context.Rates.Count();
            count.Should().Be(4);
        }
    }
}