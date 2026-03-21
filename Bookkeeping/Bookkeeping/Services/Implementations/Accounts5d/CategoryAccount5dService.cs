using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.Accounts5d;

namespace Bookkeeping.Services.Implementations.Accounts5d
{
    public class CategoryAccount5dService
        : TreeBaseService<CategoryAccount5d>, ICategoryAccount5dService
    {
        public CategoryAccount5dService(
            IPostgreSQLRepository<CategoryAccount5d> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<CategoryAccount5dService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
