using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.ReferenceBooks;

namespace Bookkeeping.Services.Implementations.ReferenceBooks
{
    public class ReferenceBookCategoryService
        : BaseService<ReferenceBookCategory>, IReferenceBookCategoryService
    {
        public ReferenceBookCategoryService(
            IPostgreSQLRepository<ReferenceBookCategory> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<ReferenceBookCategoryService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
