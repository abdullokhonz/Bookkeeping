using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetPagedIncomeCategory
{
    public class GetPagedIncomeCategoryQueryHandler
        : GetPagedBaseQueryHandler<IncomeCategory, IncomeCategoryGetDto>,
        IRequestHandler<GetPagedIncomeCategoryQuery, Result<PagedList<IncomeCategoryGetDto>>>
    {
        public GetPagedIncomeCategoryQueryHandler(
            IIncomeCategoryService service,
            IMapper mapper,
            ILogger<GetPagedIncomeCategoryQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<IncomeCategoryGetDto>>> Handle(
            GetPagedIncomeCategoryQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
