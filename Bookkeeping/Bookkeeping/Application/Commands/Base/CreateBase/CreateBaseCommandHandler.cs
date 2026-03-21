using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.CreateBase
{
    public class CreateBaseCommandHandler<TEntity, TCreateDto, TGetDto>
        : IRequestHandler<CreateBaseCommand<TEntity, TCreateDto, TGetDto>, Result<TGetDto>>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateBaseCommandHandler<TEntity, TCreateDto, TGetDto>> _logger;

        public CreateBaseCommandHandler(
            IBaseService<TEntity> service,
            IMapper mapper,
            ILogger<CreateBaseCommandHandler<TEntity, TCreateDto, TGetDto>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result<TGetDto>> Handle(
            CreateBaseCommand<TEntity, TCreateDto, TGetDto> request, CancellationToken ct)
        {
            var entity = _mapper.Map<TEntity>(request.Dto);

            var createdResult = await _service.CreateAsync(entity, ct);

            if (createdResult.IsFailure)
                return Result<TGetDto>.Failure(createdResult.Error);

            var resultDto = _mapper.Map<TGetDto>(createdResult.Value);

            return resultDto;
        }
    }
}
