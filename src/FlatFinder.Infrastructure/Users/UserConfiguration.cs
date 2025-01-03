using FlatFinder.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FlatFinder.Infrastructure.Users
{
    internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(user => user.Id);

            builder.Property(user => user.FirstName)
                .HasMaxLength(100)
                .HasConversion(firstName => firstName.value, value => new FirstName(value));

            builder.Property(user => user.LastName)
                .HasMaxLength(100)
                .HasConversion(firstName => firstName.value, value => new LastName(value));

            builder.Property(user => user.Email)
                .HasMaxLength(50)
                .HasConversion(email => email.value, value => new Email(value)); ;

            builder.HasIndex(user => user.Email).IsUnique();
        }
    }
}
