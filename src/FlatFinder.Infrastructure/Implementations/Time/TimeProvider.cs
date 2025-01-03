using FlatFinder.Application.Abstractions.Time;

namespace FlatFinder.Infrastructure.Implementations.Time
{
    internal sealed class TimeProvider : ITimeProvider
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
