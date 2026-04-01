using Bookkeeping.Entities.Users;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.Users;

namespace Bookkeeping.Services.Implementations.Users
{
    public class UserService
        : BaseService<User>, IUserService
    {
        public UserService(
            IPostgreSQLRepository<User> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<UserService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
