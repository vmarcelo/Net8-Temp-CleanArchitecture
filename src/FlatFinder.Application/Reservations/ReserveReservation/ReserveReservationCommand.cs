using FlatFinder.Application.Abstractions.CQRS;

namespace FlatFinder.Application.Reservations.ReserveReservation
{
    public record ReserveReservationCommand(
        Guid FlatId,
        Guid UserId,
        DateOnly StartDate,
        DateOnly EndDate) : ICommand<Guid>;
}
