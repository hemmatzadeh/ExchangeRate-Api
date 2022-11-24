using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NF.ExchangeRates.Core.Entities;

namespace NF.ExchangeRates.MsSql.Persistence.Configs
{
    public class RateInfoConfiguration : IEntityTypeConfiguration<RateInfo>
    {
        public void Configure(EntityTypeBuilder<RateInfo> builder)
        {
            builder.ToTable("RateInfo");

            builder.Property(e=>e.Rate).HasPrecision(19, 9);

            builder.Property(e=>e.Created)
                .HasColumnType("datetime");

            builder.Property(e => e.BaseCurrency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.ToCurrency)
                .IsRequired()
                .HasMaxLength(3);
        }
    }
}
