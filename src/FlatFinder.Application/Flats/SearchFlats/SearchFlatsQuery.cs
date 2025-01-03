using FlatFinder.Application.Abstractions.CQRS;

namespace FlatFinder.Application.Flats.SearchFlats
{
    public sealed record SearchFlatsQuery(DateOnly StartDate, DateOnly EndDate) 
        : IQuery<IReadOnlyList<FlatResponse>>;
}
