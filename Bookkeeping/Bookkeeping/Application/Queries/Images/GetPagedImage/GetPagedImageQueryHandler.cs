using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetPagedBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.Images.GetPagedImage
{
    public class GetPagedImageQueryHandler
        : GetPagedBaseQueryHandler<Image, ImageGetDto>,
        IRequestHandler<GetPagedImageQuery, Result<PagedList<ImageGetDto>>>
    {
        public GetPagedImageQueryHandler(
            IImageService service,
            IMapper mapper,
            ILogger<GetPagedImageQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<PagedList<ImageGetDto>>> Handle(
            GetPagedImageQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
