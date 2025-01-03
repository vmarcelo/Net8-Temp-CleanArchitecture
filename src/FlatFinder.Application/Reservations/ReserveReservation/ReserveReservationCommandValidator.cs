using FluentValidation;

namespace FlatFinder.Application.Reservations.ReserveReservation
{
    public class ReserveReservationCommandValidator : AbstractValidator<ReserveReservationCommand>
    {
        public ReserveReservationCommandValidator()
        {
            RuleFor(x => x.FlatId).NotEmpty();
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate)
                .WithMessage("Hey las fechas no pueden ser iguales, además la fecha inicio debe ser anterior...");
        }
    }
}
