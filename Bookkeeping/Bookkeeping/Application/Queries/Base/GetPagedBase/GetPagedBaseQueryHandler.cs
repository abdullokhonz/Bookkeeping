using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Queries.Base.GetPagedBase
{
    public class GetPagedBaseQueryHandler<TEntity, TResponse>
        : IRequestHandler<GetPagedBaseQuery<TEntity, TResponse>, Result<PagedList<TResponse>>>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<GetPagedBaseQueryHandler<TEntity, TResponse>> _logger;

        public GetPagedBaseQueryHandler(
            IBaseService<TEntity> service,
            IMapper mapper,
            ILogger<GetPagedBaseQueryHandler<TEntity, TResponse>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<Result<PagedList<TResponse>>> Handle(
            GetPagedBaseQuery<TEntity, TResponse> request, CancellationToken ct)
        {
            var pagedEntitiesResult = await _service.GetPagedAsync(request.Page, request.Size, ct);

            if (pagedEntitiesResult.IsFailure)
                return Result<PagedList<TResponse>>.Failure(pagedEntitiesResult.Error);

            var dtos = _mapper.Map<IReadOnlyList<TResponse>>(pagedEntitiesResult.Value.Items);

            var pagedDtoList = new PagedList<TResponse>(
                dtos,
                pagedEntitiesResult.Value.TotalCount,
                pagedEntitiesResult.Value.Page,
                pagedEntitiesResult.Value.PageSize
            );

            return pagedDtoList;
        }
    }
}
