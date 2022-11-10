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
        public DbSet<UserExchangeInfo> userExchangeInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new RateInfoConfiguration());
            modelBuilder.ApplyConfiguration(new UserExchangeConfiguration());
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
