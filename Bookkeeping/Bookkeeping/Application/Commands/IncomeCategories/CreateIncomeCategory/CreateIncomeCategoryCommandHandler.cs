using AutoMapper;
using Bookkeeping.Application.Commands.Base.CreateBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.IncomeCategories.CreateIncomeCategory
{
    public class CreateIncomeCategoryCommandHandler
        : CreateBaseCommandHandler<IncomeCategory, IncomeCategoryCreateDto, IncomeCategoryGetDto>,
        IRequestHandler<CreateIncomeCategoryCommand, Result<IncomeCategoryGetDto>>
    {
        public CreateIncomeCategoryCommandHandler(
            IIncomeCategoryService service,
            IMapper mapper,
            ILogger<CreateIncomeCategoryCommandHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IncomeCategoryGetDto>> Handle(
            CreateIncomeCategoryCommand request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
