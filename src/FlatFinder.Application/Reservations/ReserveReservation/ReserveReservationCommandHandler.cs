using FlatFinder.Application.Abstractions.CQRS;
using FlatFinder.Application.Abstractions.Time;
using FlatFinder.Application.Exceptions;
using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Users;

namespace FlatFinder.Application.Reservations.ReserveReservation
{
    internal sealed class ReserveReservationCommandHandler : ICommandHandler<ReserveReservationCommand, Guid>
    {
        private readonly IUserRepository userRepository;
        private readonly IFlatRepository flatRepository;
        private readonly IReservationRepository reservationRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly PricingService pricingService;
        private readonly ITimeProvider timeProvider;

        public ReserveReservationCommandHandler(
            IUserRepository userRepository,
            IFlatRepository flatRepository,
            IReservationRepository reservationRepository,
            IUnitOfWork unitOfWork,
            PricingService pricingService,
            ITimeProvider timeProvider)
        {
            this.userRepository = userRepository;
            this.flatRepository = flatRepository;
            this.reservationRepository = reservationRepository;
            this.unitOfWork = unitOfWork;
            this.pricingService = pricingService;
            this.timeProvider = timeProvider;
        }
        public async Task<Result<Guid>> Handle(ReserveReservationCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user is null)
                return Result.Failure<Guid>(UserErrors.NotFound);

            var flat = await flatRepository.GetByIdAsync(request.FlatId, cancellationToken);

            if (flat is null)
                return Result.Failure<Guid>(FlatErrors.NotFound);

            var duration = DateRange.Create(request.StartDate, request.EndDate);

            if (await reservationRepository.IsOverlappingAsync(flat, duration, cancellationToken))
                return Result.Failure<Guid>(ReservationErrors.Overlap);

            try
            {
                var reservation = Reservation.Reserve(
                    flat,
                    user.Id,
                    duration,
                    timeProvider.UtcNow,
                    pricingService);

                reservationRepository.Add(reservation);
                await unitOfWork.SaveChangesAsync(cancellationToken);
                return reservation.Id;
            }
            catch (PersistenceConcurrencyException)
            {
                return Result.Failure<Guid>(ReservationErrors.Overlap);
            }
        }
    }
}
