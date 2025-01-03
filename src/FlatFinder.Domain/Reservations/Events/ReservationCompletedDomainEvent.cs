using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reservations.Events
{
    public sealed record ReservationCompletedDomainEvent(Guid ReservationId):IDomainEvent;
}
