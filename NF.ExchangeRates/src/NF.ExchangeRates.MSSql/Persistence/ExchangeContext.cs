using Microsoft.EntityFrameworkCore;
using NF.ExchangeRates.Core.Entities;

namespace NF.ExchangeRates.MsSql.Persistence
{
    public class ExchangeContext : DbContext
    {
        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
        {
        }

        public DbSet<RateInfo> Rates { get; set; }
    }
}
