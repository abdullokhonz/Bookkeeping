using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetAllBase
{
    public class GetAllBaseQueryHandler<TEntity, TGetDto>
        : IRequestHandler<GetAllBaseQuery<TEntity, TGetDto>, Result<IEnumerable<TGetDto>>>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GetAllBaseQueryHandler<TEntity, TGetDto>> _logger;

        public GetAllBaseQueryHandler(
            IBaseService<TEntity> service,
            IMapper mapper,
            ILogger<GetAllBaseQueryHandler<TEntity, TGetDto>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result<IEnumerable<TGetDto>>> Handle(
            GetAllBaseQuery<TEntity, TGetDto> request, CancellationToken ct)
        {
            var entitiesResult = await _service.GetAllAsync(ct);

            if (entitiesResult.IsFailure)
                return Result<IEnumerable<TGetDto>>.Failure(entitiesResult.Error);

            var resultDtos = _mapper.Map<IEnumerable<TGetDto>>(entitiesResult.Value);

            return Result<IEnumerable<TGetDto>>.Success(resultDtos);
        }
    }
}
