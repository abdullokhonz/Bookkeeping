using Bookkeeping.Application.Commands.Base.DeleteBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.IncomeCategories.DeleteIncomeCategory
{
    public class DeleteIncomeCategoryCommandHandler
        : DeleteBaseCommandHandler<IncomeCategory>,
        IRequestHandler<DeleteIncomeCategoryCommand, Result>
    {
        public DeleteIncomeCategoryCommandHandler(
            IIncomeCategoryService service,
            ILogger<DeleteIncomeCategoryCommandHandler> logger)
            : base(service, logger)
        {

        }

        public async Task<Result> Handle
            (DeleteIncomeCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
