using FlatFinder.Domain.Abstractions;

namespace FlatFinder.Domain.Reviews
{
    public static class ReviewErrors
    {
        public static readonly Error NotEligible = new("Review.NotEligible",
            "You can not make a review because you didn't reserved the flat.");
    }
}
