using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reservations
{
    public static class ReservationErrors
    {
        public static Error NotFound = new Error(
            "Reservation.Found",
            "The reservation with the specified id was not found");

        public static Error NotReserved = new Error(
            "Reservation.NotReserved",
            "Reservation not pending");

        public static Error Overlap = new Error(
            "Reservation.Overlap",
            "The reservation is overlapping with an existing one");

        public static Error NotConfirmed = new Error(
            "Reservation.NotConfirmed",
            "The reservation is not confirmed");

        public static Error AlreadyStarted = new Error(
            "Reservation.AlreadyStarted",
            "The reservation has already started.");
    }
}
