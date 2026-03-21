using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using MediatR;

namespace Bookkeeping.Application.Commands.Images.UpdateImage
{
    public class UpdateImageCommandHandler
        : IRequestHandler<UpdateImageCommand, Result<ImageGetDto>>
    {
        private readonly IImageService _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateImageCommandHandler> _logger;

        public UpdateImageCommandHandler(
            IImageService service,
            IMapper mapper,
            ILogger<UpdateImageCommandHandler> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<ImageGetDto>> Handle(
            UpdateImageCommand request, CancellationToken ct)
        {
            // Пробуем обновить (внутри сервиса есть проверка на существование)
            var updateResult = await _service.UpdateImageAsync(request.Id, request.Dto, request.File, ct);

            if (updateResult.IsFailure)
                return Result<ImageGetDto>.Failure(updateResult.Error);

            // Получаем обновленную сущность
            var updatedEntityResult = await _service.GetByIdAsync(request.Id, ct);

            if (updatedEntityResult.IsFailure)
                return Result<ImageGetDto>.Failure(updatedEntityResult.Error);

            // Возвращаем DTO
            return _mapper.Map<ImageGetDto>(updatedEntityResult.Value);
        }
    }
}
