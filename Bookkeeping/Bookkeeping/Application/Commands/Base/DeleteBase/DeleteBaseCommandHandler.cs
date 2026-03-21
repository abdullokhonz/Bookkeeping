using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.DeleteBase
{
    public class DeleteBaseCommandHandler<TEntity>
        : IRequestHandler<DeleteBaseCommand<TEntity>, Result>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly ILogger<DeleteBaseCommandHandler<TEntity>> _logger;

        public DeleteBaseCommandHandler(
            IBaseService<TEntity> service,
            ILogger<DeleteBaseCommandHandler<TEntity>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result> Handle(
            DeleteBaseCommand<TEntity> request, CancellationToken ct)
        {
            return await _service.DeleteAsync(request.Id, ct);
        }
    }
}
