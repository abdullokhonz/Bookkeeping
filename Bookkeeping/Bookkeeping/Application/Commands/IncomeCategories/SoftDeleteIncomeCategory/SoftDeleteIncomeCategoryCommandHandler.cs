using Bookkeeping.Application.Commands.Base.SoftDeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.IncomeCategories.SoftDeleteIncomeCategory
{
    public class SoftDeleteIncomeCategoryCommandHandler
        : SoftDeleteBaseCommandHandler<IncomeCategory>,
        IRequestHandler<SoftDeleteIncomeCategoryCommand, Result>
    {
        public SoftDeleteIncomeCategoryCommandHandler(
            IIncomeCategoryService service,
            ILogger<SoftDeleteIncomeCategoryCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle(
            SoftDeleteIncomeCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
