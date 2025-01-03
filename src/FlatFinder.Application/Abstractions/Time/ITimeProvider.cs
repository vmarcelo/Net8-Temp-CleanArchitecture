namespace FlatFinder.Application.Abstractions.Time
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
    }
}
