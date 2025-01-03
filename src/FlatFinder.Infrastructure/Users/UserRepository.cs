using FlatFinder.Domain.Users;
using FlatFinder.Infrastructure.Shared;

namespace FlatFinder.Infrastructure.Users
{
    internal sealed class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
