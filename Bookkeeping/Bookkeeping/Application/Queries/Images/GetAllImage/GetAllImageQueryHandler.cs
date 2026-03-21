using AutoMapper;
using Bookkeeping.Application.Queries.Base.GetAllBase;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Queries.Images.GetAllImage
{
    public class GetAllImageQueryHandler
        : GetAllBaseQueryHandler<Image, ImageGetDto>,
        IRequestHandler<GetAllImageQuery, Result<IEnumerable<ImageGetDto>>>
    {
        public GetAllImageQueryHandler(
            IImageService service,
            IMapper mapper,
            ILogger<GetAllImageQueryHandler> logger)
            : base(service, mapper, logger)
        {

        }

        public async Task<Result<IEnumerable<ImageGetDto>>> Handle(
            GetAllImageQuery request, CancellationToken ct)
        {
            return await base.Handle(request, ct);
        }
    }
}
