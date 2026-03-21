using AutoMapper;
using Bookkeeping.Application.Commands.Base.UpdateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.IncomeCategories.UpdateIncomeCategory
{
    public class UpdateIncomeCategoryCommandHandler
        : UpdateBaseCommandHandler<IncomeCategory, IncomeCategoryUpdateDto>,
        IRequestHandler<UpdateIncomeCategoryCommand, Result>
    {
        public UpdateIncomeCategoryCommandHandler(
            IIncomeCategoryService service,
            IMapper mapper,
            ILogger<UpdateIncomeCategoryCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result> Handle(
            UpdateIncomeCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
