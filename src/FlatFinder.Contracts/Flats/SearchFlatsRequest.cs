namespace FlatFinder.Contracts.Flats;

public sealed record SearchFlatsRequest(DateOnly startDate, DateOnly endDate);
