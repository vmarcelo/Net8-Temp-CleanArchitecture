using FlatFinder.Application.Abstractions.CQRS;

namespace FlatFinder.Application.Reservations.GetReservation
{
    public sealed record GetReservationQuery(Guid ReservationId) : IQuery<ReservationResponse>;
}
