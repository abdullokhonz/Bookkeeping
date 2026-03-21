using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetBaseById
{
    public class GetBaseByIdQueryHandler<TEntity, TGetDto>
        : IRequestHandler<GetBaseByIdQuery<TEntity, TGetDto>, Result<TGetDto>>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GetBaseByIdQueryHandler<TEntity, TGetDto>> _logger;

        public GetBaseByIdQueryHandler(
            IBaseService<TEntity> service,
            IMapper mapper,
            ILogger<GetBaseByIdQueryHandler<TEntity, TGetDto>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result<TGetDto>> Handle(
            GetBaseByIdQuery<TEntity, TGetDto> request, CancellationToken ct)
        {
            var entityResult = await _service.GetByIdAsync(request.Id, ct);

            if (entityResult.IsFailure)
                return Result<TGetDto>.Failure(entityResult.Error);

            return _mapper.Map<TGetDto>(entityResult.Value);
        }
    }
}
