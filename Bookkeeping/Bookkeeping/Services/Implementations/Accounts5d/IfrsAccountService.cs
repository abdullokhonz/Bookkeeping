using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.Accounts5d;

namespace Bookkeeping.Services.Implementations.Accounts5d
{
    public class IfrsAccountService
        : TreeBaseService<IfrsAccount>, IIfrsAccountService
    {
        public IfrsAccountService(
            IPostgreSQLRepository<IfrsAccount> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<IfrsAccountService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
