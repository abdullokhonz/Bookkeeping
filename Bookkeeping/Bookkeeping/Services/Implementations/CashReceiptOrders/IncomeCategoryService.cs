using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;

namespace Bookkeeping.Services.Implementations.CashReceiptOrders
{
    public class IncomeCategoryService
        : BaseService<IncomeCategory>, IIncomeCategoryService
    {
        public IncomeCategoryService(
            IPostgreSQLRepository<IncomeCategory> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<IncomeCategoryService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
