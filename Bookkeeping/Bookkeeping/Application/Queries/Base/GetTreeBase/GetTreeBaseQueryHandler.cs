using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetTreeBase
{
    public class GetTreeBaseQueryHandler<TEntity, TResponse>
        : IRequestHandler<GetTreeBaseQuery<TEntity, TResponse>, Result<IEnumerable<TResponse>>>
        where TEntity : BaseEntity, ITreeEntity<TEntity>
    {
        private readonly ITreeBaseService<TEntity> _treeService;
        private readonly IMapper _mapper;
        private readonly ILogger<GetTreeBaseQueryHandler<TEntity, TResponse>> _logger;

        public GetTreeBaseQueryHandler(
            ITreeBaseService<TEntity> treeService,
            IMapper mapper,
            ILogger<GetTreeBaseQueryHandler<TEntity, TResponse>> logger)
        {
            _treeService = treeService;
            _mapper = mapper;
            _logger = logger;
        }

        public virtual async Task<Result<IEnumerable<TResponse>>> Handle(GetTreeBaseQuery<TEntity, TResponse> request, CancellationToken ct)
        {
            var entitiesResult = await _treeService.GetTreeAsync(ct);

            if (entitiesResult.IsFailure)
                return Result<IEnumerable<TResponse>>.Failure(entitiesResult.Error);

            var dtos = _mapper.Map<IEnumerable<TResponse>>(entitiesResult.Value);

            return Result<IEnumerable<TResponse>>.Success(dtos);
        }
    }
}
