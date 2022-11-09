using Microsoft.EntityFrameworkCore;
using NF.ExchangeRates.Core.Entities;
using NF.ExchangeRates.MsSql.Persistence.Configs;

namespace NF.ExchangeRates.MsSql.Persistence
{
    public partial class ExchangeContext : DbContext
    {
        public ExchangeContext(DbContextOptions<ExchangeContext> options) : base(options)
        {
        }

        public DbSet<RateInfo> Rates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RateInfoConfiguration());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
