using FlatFinder.Domain.Abstractions;
using FlatFinder.Domain.Reservations;
using FlatFinder.Domain.Reviews.Events;

namespace FlatFinder.Domain.Reviews
{
    public sealed class Review : Entity
    {
        private Review(
         Guid id,
         Guid flatId,
         Guid reservationId,
         Guid userId,
         Rating rating,
         Comment comment,
         DateTime createdOnUtc)
         : base(id)
        {
            FlatId = flatId;
            ReservationId = reservationId;
            UserId = userId;
            Rating = rating;
            Comment = comment;
            CreatedOnUtc = createdOnUtc;
        }

        public Guid FlatId { get; private set; }

        public Guid ReservationId { get; private set; }

        public Guid UserId { get; private set; }

        public Rating Rating { get; private set; }

        public Comment Comment { get; private set; }

        public DateTime CreatedOnUtc { get; private set; }

        public static Result<Review> Create(
            Reservation reservation,
            Rating rating,
            Comment comment,
            DateTime createdOnUtc)
        {
            if (reservation.Status != ReservationStatus.Completed)
            {
                return Result.Failure<Review>(ReviewErrors.NotEligible);
            }

            var review = new Review(
                Guid.NewGuid(),
                reservation.FlatId,
                reservation.Id,
                reservation.UserId,
                rating,
                comment,
                createdOnUtc);

            review.RaiseDomainEvent(new ReviewCreatedDomainEvent(review.Id));

            return review;
        }

        private Review()
        {

        }
    }
}
