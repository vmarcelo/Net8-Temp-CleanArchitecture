using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reviews.Events
{
    public sealed record ReviewCreatedDomainEvent(Guid ReviewId) : IDomainEvent;
}
