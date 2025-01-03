using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Reviews;
using FlatFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatFinder.Infrastructure.Reviews
{
    internal sealed class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("reviews");
            builder.HasKey(x => x.Id);
            builder.HasOne<Flat>()
                .WithMany()
                .HasForeignKey(x => x.FlatId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne<Reservation>()
                .WithMany()
                .HasForeignKey(x => x.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.Rating)
                .HasConversion(x => x.Value, value => Rating.Create(value).Value);

            builder.Property(x => x.Comment)
                .HasMaxLength(150)
                .HasConversion(x => x.Value, value => new Comment(value));
        }
    }
}
