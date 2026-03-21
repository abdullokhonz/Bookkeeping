using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetBaseById;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.Images.GetImageById
{
    public class GetImageByIdQueryHandler
        : GetBaseByIdQueryHandler<Image, ImageGetDto>,
        IRequestHandler<GetImageByIdQuery, Result<ImageGetDto>>
    {
        public GetImageByIdQueryHandler(
            IImageService service,
            IMapper mapper,
            ILogger<GetImageByIdQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<ImageGetDto>> Handle(
            GetImageByIdQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
