using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reservations.Events
{
    public sealed record ReservationCancelledDomainEvent(Guid ReservationId) : IDomainEvent;
}
