using FlatFinder.Domain.Flats;
using FlatFinder.Infrastructure.Shared;

namespace FlatFinder.Infrastructure.Flats
{
    internal sealed class FlatRepository : RepositoryBase<Flat>, IFlatRepository
    {
        public FlatRepository(ApplicationDbContext context) : base(context)
        {
        }

    }
}
