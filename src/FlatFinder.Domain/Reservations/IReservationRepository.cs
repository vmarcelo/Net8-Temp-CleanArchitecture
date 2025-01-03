using FlatFinder.Domain.Flats;

namespace FlatFinder.Domain.Reservations
{
    public interface IReservationRepository
    {
        Task<Reservation?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        Task<bool> IsOverlappingAsync(Flat flat, DateRange duration,  CancellationToken cancellationToken = default);
        void Add(Reservation reservation);
    }
}
