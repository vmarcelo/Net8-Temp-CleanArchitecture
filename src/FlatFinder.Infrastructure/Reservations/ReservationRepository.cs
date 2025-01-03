using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations;
using FlatFinder.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace FlatFinder.Infrastructure.Reservations
{
    internal sealed class ReservationRepository : RepositoryBase<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        private static readonly ReservationStatus[] ActiveReservationStatuses =
        {
            ReservationStatus.Reserved,
            ReservationStatus.Confirmed,
            ReservationStatus.Completed
        };

        public async Task<bool> IsOverlappingAsync(
            Flat flat,
            DateRange duration,
            CancellationToken cancellationToken = default)
        {
            return await context
                .Set<Reservation>()
                .AnyAsync(
                    x =>
                        x.FlatId == flat.Id &&
                        x.Duration.Start <= duration.End &&
                        x.Duration.End >= duration.Start &&
                        ActiveReservationStatuses.Contains(x.Status),
                    cancellationToken
                );
        }
    }
}
