using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Users.Events
{
    public sealed record UserCreatedDomainEvent(Guid UserId) : IDomainEvent;
}
