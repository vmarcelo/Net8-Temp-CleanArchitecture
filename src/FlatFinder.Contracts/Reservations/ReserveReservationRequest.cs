namespace FlatFinder.Contracts.Reservations
{
    public sealed record ReserveReservationRequest(
        Guid FlatId,
        Guid UserId,
        DateOnly StartDate,
        DateOnly EndDate);
}
