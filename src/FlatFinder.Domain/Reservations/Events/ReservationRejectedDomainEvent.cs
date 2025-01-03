using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reservations.Events
{
    public sealed record ReservationRejectedDomainEvent(Guid ReservationId):IDomainEvent;
}
