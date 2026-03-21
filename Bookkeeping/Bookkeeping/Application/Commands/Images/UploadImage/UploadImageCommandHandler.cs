using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.UploadImage
{
    public class UploadImageCommandHandler
        : IRequestHandler<UploadImageCommand, Result<ImageGetDto>>
    {
        private readonly IImageService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UploadImageCommandHandler> _logger;

        public UploadImageCommandHandler(
            IImageService service,
            IMapper mapper,
            ILogger<UploadImageCommandHandler> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<ImageGetDto>> Handle(
            UploadImageCommand request, CancellationToken ct)
        {
            var createdResult = await _service.UploadImageAsync(request.Dto, request.File, ct);

            if (createdResult.IsFailure) return Result<ImageGetDto>.Failure(createdResult.Error);

            return _mapper.Map<ImageGetDto>(createdResult.Value);
        }
    }
}
