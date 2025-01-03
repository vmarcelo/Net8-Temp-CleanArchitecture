using FlatFinder.Application.Abstractions.Communication;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Reservations.Events;
using FlatFinder.Domain.Users;
using MediatR;

namespace FlatFinder.Application.Reservations.ReserveReservation
{
    internal sealed class ReservationReservedDomainEventHandler : INotificationHandler<ReservationRejectedDomainEvent>
    {
        private readonly IReservationRepository reservationRepository;
        private readonly IUserRepository userRepository;
        private readonly IEmailService emailService;

        public ReservationReservedDomainEventHandler(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            IEmailService emailService)
        {
            this.reservationRepository = reservationRepository;
            this.userRepository = userRepository;
            this.emailService = emailService;
        }
        public async Task Handle(ReservationRejectedDomainEvent notification, CancellationToken cancellationToken)
        {
            var reservation = await reservationRepository
                .GetByIdAsync(notification.ReservationId, cancellationToken);

            if (reservation is null)
                return;

            var user = await userRepository.GetByIdAsync(reservation.UserId, cancellationToken);

            if (user is null)
                return;

            await emailService.SendAsync(
                user.Email,
                "The flat has been reserved",
                "Please note that you have 15 minutes to confirm this booking");
        }
    }
}
