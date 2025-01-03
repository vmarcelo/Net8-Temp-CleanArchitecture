using FlatFinder.Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FlatFinder.Infrastructure.Shared
{
    internal abstract class RepositoryBase<T> where T : Entity
    {
        protected readonly ApplicationDbContext context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await context.Set<T>().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public void Add(T entity)
        {
            context.Add(entity);
        }
    }
}
