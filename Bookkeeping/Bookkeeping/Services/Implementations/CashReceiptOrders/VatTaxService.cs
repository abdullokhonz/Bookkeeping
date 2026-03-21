using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;

namespace Bookkeeping.Services.Implementations.CashReceiptOrders
{
    public class VatTaxService
        : BaseService<VatTax>, IVatTaxService
    {
        public VatTaxService(
            IPostgreSQLRepository<VatTax> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<VatTaxService> logger)
            : base(repository, config, context, logger)
        {

        }
    }
}
