using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetIncomeCategoryById
{
    public class GetIncomeCategoryByIdQueryHandler
        : GetBaseByIdQueryHandler<IncomeCategory, IncomeCategoryGetDto>,
        IRequestHandler<GetIncomeCategoryByIdQuery, Result<IncomeCategoryGetDto>>
    {
        public GetIncomeCategoryByIdQueryHandler(
            IIncomeCategoryService service,
            IMapper mapper,
            ILogger<GetIncomeCategoryByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IncomeCategoryGetDto>> Handle(
            GetIncomeCategoryByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
