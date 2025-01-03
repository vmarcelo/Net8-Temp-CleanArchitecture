using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatFinder.Infrastructure.Flats
{
    internal sealed class FlatConfiguration : IEntityTypeConfiguration<Flat>
    {
        public void Configure(EntityTypeBuilder<Flat> builder)
        {
            builder.ToTable("flats");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .HasConversion(x => x.value, value => new Name(value));

            builder.Property(x => x.Description)
                .HasMaxLength(500)
                .HasConversion(x => x.value, value => new Description(value));

            builder.OwnsOne(x => x.Address);

            builder.OwnsOne(x => x.Price, priceBuilder =>
            {
                priceBuilder.Property(x => x.Currency)
                    .HasConversion(x => x.Code, code => Currency.FromCode(code));
                priceBuilder.Property(x => x.Amount)
                    //.HasPrecision(3);
                    .HasColumnType("decimal(10,2)");//precisión y escala específico mediante indicar el tipo
            });

            builder.OwnsOne(x => x.CleaningFee, priceBuilder =>
            {
                priceBuilder.Property(x => x.Currency)
                    .HasConversion(x => x.Code, code => Currency.FromCode(code));
                priceBuilder.Property(x => x.Amount)
                    .HasColumnType("decimal(10,2)");
            });

            builder.Property<byte[]>("Version").IsRowVersion();//shadow property used for optimistic concurrency

            //Seed data
            builder.HasData(
                new
                {
                    Id = Guid.Parse("9b874476-d3ed-430d-8dfb-934582487dc1"),
                    Name = new Name("Flat 1"), // Descomponiendo Name
                    Description = new Description("This is a sample flat description"), // Descomponiendo Description
                    Amenities = new List<Amenity> { Amenity.WiFi, Amenity.Parking, Amenity.Gym }
                }
            );

            builder.OwnsOne(x => x.Address).HasData(
                new
                {
                    FlatId = Guid.Parse("9b874476-d3ed-430d-8dfb-934582487dc1"), // Relación con Flat
                    Country = "Peru",
                    State = "Lima",
                    ZipCode = "Lima01",
                    City = "San Miguel",
                    Street = "Calle 1"
                }
            );

            builder.OwnsOne(x => x.CleaningFee).HasData(
                new
                {
                    FlatId = Guid.Parse("9b874476-d3ed-430d-8dfb-934582487dc1"), // Relación con Flat
                    Amount = 5m,
                    Currency = new Currency("PEN")
                }
            );
            builder.OwnsOne(x => x.Price).HasData(
                new
                {
                    FlatId = Guid.Parse("9b874476-d3ed-430d-8dfb-934582487dc1"), // Relación con Flat
                    Amount = 50m,
                    Currency = new Currency("PEN")
                }
            );

        }
    }
}
