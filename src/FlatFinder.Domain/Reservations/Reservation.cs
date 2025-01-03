using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Flats;
using FlatFinder.Domain.Reservations.Events;
using FlatFinder.Domain.Shared;

namespace FlatFinder.Domain.Reservations
{
    public sealed class Reservation : Entity
    {
        private Reservation(
            Guid id,
            Guid flatId,
            Guid userId,
            DateRange duration,
            //PricingDetails pricingDetails,
            Money priceForPeriod,            
            Money cleaningFee,
            Money amenitiesUpCharge,
            Money totalPrice,
            ReservationStatus status,
            DateTime createdOnUtc
            ) : base(id)
        {
            FlatId = flatId;
            UserId = userId;
            Duration = duration;
            //PricingDetails = pricingDetails;
            PriceForPeriod = priceForPeriod;
            CleaningFee = cleaningFee;
            AmenitiesUpCharge = amenitiesUpCharge;
            TotalPrice = totalPrice;
            Status = status;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid FlatId { get; private set; }
        public Guid UserId { get; private set; }
        public DateRange Duration { get; private set; }
        //public PricingDetails PricingDetails { get; private set; }
        public Money PriceForPeriod { get; private set; }
        public Money CleaningFee { get; private set; }
        public Money AmenitiesUpCharge { get; private set; }
        public Money TotalPrice { get; private set; }
        public ReservationStatus Status { get; private set; }
        public DateTime CreatedOnUtc { get; private set; }
        public DateTime? ConfirmedOnUtc { get; private set; }
        public DateTime? RejectedOnUtc { get; private set; }
        public DateTime? CompletedOnUtc { get; private set; }
        public DateTime? CancelledOnUtc { get; private set; }

        public static Reservation Reserve(
            Flat flat, 
            Guid userId, 
            DateRange duration, 
            DateTime utcNow,
            PricingService pricingService)
        {
            var pricingDetails = pricingService.CalculatePrice(flat, duration);
            var reservation = new Reservation(
                Guid.NewGuid(), 
                flat.Id, 
                userId,
                duration,
                //pricingDetails,
                pricingDetails.PriceForPeriod,
                pricingDetails.CleaningFee,
                pricingDetails.AmenitiesUpCharge,
                pricingDetails.TotalPrice,
                ReservationStatus.Reserved,
                utcNow                
                );
            reservation.RaiseDomainEvent(new ReservationReservedDomainEvent(reservation.Id));
            flat.LastBookedOnUtc = utcNow;
            return reservation;
        }

        public Result Confirm(DateTime utcNow)
        {
            if(Status != ReservationStatus.Reserved)
            {
                return Result.Failure(ReservationErrors.NotReserved);
            }

            Status = ReservationStatus.Confirmed;
            ConfirmedOnUtc = utcNow;
            RaiseDomainEvent(new ReservationConfirmedDomainEvent(Id));
            return Result.Success();
        }
        public Result Reject(DateTime utcNow)
        {
            if (Status != ReservationStatus.Reserved)
            {
                return Result.Failure(ReservationErrors.NotReserved);
            }

            Status = ReservationStatus.Rejected;
            RejectedOnUtc = utcNow;

            RaiseDomainEvent(new ReservationRejectedDomainEvent(Id));

            return Result.Success();
        }

        public Result Complete(DateTime utcNow)
        {
            if (Status != ReservationStatus.Confirmed)
            {
                return Result.Failure(ReservationErrors.NotConfirmed);
            }

            Status = ReservationStatus.Completed;
            CompletedOnUtc = utcNow;

            RaiseDomainEvent(new ReservationCompletedDomainEvent(Id));

            return Result.Success();
        }

        public Result Cancel(DateTime utcNow)
        {
            if (Status != ReservationStatus.Confirmed)
            {
                return Result.Failure(ReservationErrors.NotConfirmed);
            }

            var currentDate = DateOnly.FromDateTime(utcNow);

            if (currentDate > Duration.Start)
            {
                return Result.Failure(ReservationErrors.AlreadyStarted);
            }

            Status = ReservationStatus.Cancelled;
            CancelledOnUtc = utcNow;

            RaiseDomainEvent(new ReservationCancelledDomainEvent(Id));

            return Result.Success();
        }

        private Reservation()
        {

        }
    }
}
