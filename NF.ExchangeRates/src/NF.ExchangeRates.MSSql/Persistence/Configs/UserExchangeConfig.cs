using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NF.ExchangeRates.Core.Entities;

namespace NF.ExchangeRates.MsSql.Persistence.Configs
{
    internal class UserExchangeConfiguration : IEntityTypeConfiguration<UserExchangeInfo>
    {
        public void Configure(EntityTypeBuilder<UserExchangeInfo> builder)
        {
            builder.ToTable("UserExchangeInfo");

            builder.Property(e => e.Created)
                .HasColumnType("datetime");

            builder.Property(e => e.BaseCurrency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e => e.ToCurrency)
                .IsRequired()
                .HasMaxLength(3);

            builder.Property(e=>e.Amount)
                .IsRequired();
        }
    }
}
