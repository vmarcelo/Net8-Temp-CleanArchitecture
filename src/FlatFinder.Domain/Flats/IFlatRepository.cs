namespace FlatFinder.Domain.Flats
{
    public interface IFlatRepository
    {
        Task<Flat?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    }
}
