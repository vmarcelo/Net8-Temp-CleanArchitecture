using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reservations.Events
{
    public sealed record ReservationReservedDomainEvent(Guid ReservationId) : IDomainEvent;
}
