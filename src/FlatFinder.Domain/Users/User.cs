using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Users.Events;

namespace FlatFinder.Domain.Users
{
    public sealed class User : Entity
    {
        private User(
            Guid id,
            FirstName firstName,
            LastName lastName,
            Email email
            ) : base(id)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public FirstName FirstName { get; private set; } = default!;
        public LastName LastName { get; private set; } = default!;
        public Email Email { get; private set; } = default!;

        public static User Create(FirstName firstName, LastName lastName, Email email)
        {
            var user = new User(Guid.NewGuid(), firstName, lastName, email);
            user.RaiseDomainEvent(new UserCreatedDomainEvent(user.Id));
            return user;
        }

        private User()
        {

        }
    }
}
