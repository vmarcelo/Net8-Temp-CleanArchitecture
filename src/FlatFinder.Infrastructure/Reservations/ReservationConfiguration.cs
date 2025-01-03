using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Shared;
using FlatFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatFinder.Infrastructure.Reservations
{
    internal sealed class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("reservations");
            builder.HasKey(x => x.Id);

            builder.HasOne<Flat>()
                .WithMany()
                .HasForeignKey(x => x.FlatId);                

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId);

            builder.OwnsOne(x => x.Duration);

            builder.OwnsOne(x => x.PriceForPeriod, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                priceBuilder.Property(money => money.Amount)
                    .HasColumnType("decimal(10,2)");
            });

            builder.OwnsOne(x => x.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                priceBuilder.Property(money => money.Amount)
                .HasColumnType("decimal(10,2)");
            });

            builder.OwnsOne(x => x.AmenitiesUpCharge, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                priceBuilder.Property(money => money.Amount)
                    .HasColumnType("decimal(10,2)");
            });

            builder.OwnsOne(x => x.TotalPrice, priceBuilder =>
            {
                priceBuilder.Property(money => money.Currency)
                    .HasConversion(currency => currency.Code, code => Currency.FromCode(code));
                priceBuilder.Property(money => money.Amount)
                    .HasColumnType("decimal(10,2)");
            });
        }
    }
}
