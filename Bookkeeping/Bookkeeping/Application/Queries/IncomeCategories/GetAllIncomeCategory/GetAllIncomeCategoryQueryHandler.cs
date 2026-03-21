using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.IncomeCategories.GetAllIncomeCategory
{
    public class GetAllIncomeCategoryQueryHandler
        : GetAllBaseQueryHandler<IncomeCategory, IncomeCategoryGetDto>,
        IRequestHandler<GetAllIncomeCategoryQuery, Result<IEnumerable<IncomeCategoryGetDto>>>
    {
        public GetAllIncomeCategoryQueryHandler(
            IIncomeCategoryService service,
            IMapper mapper,
            ILogger<GetAllIncomeCategoryQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<IncomeCategoryGetDto>>> Handle(
            GetAllIncomeCategoryQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
