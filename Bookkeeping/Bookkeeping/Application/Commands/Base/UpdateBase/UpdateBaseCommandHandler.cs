using AutoMapper;
using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.UpdateBase
{
    public class UpdateBaseCommandHandler<TEntity, TUpdateDto>
        : IRequestHandler<UpdateBaseCommand<TEntity, TUpdateDto>, Result>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateBaseCommandHandler<TEntity, TUpdateDto>> _logger;

        public UpdateBaseCommandHandler(
            IBaseService<TEntity> service,
            IMapper mapper,
            ILogger<UpdateBaseCommandHandler<TEntity, TUpdateDto>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result> Handle(
            UpdateBaseCommand<TEntity, TUpdateDto> request, CancellationToken ct)
        {
            var entityResult = await _service.GetByIdAsync(request.Id, ct);

            if (entityResult.IsFailure)
                return Result.Failure(entityResult.Error);

            _mapper.Map(request.Dto, entityResult.Value);

            return await _service.UpdateAsync(request.Id, entityResult.Value, ct);
        }
    }
}
