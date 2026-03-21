using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.SoftDeleteBase
{
    public class SoftDeleteBaseCommandHandler<TEntity>
        : IRequestHandler<SoftDeleteBaseCommand<TEntity>, Result>
        where TEntity : BaseEntity
    {
        private readonly IBaseService<TEntity> _service;
        private readonly ILogger<SoftDeleteBaseCommandHandler<TEntity>> _logger;

        public SoftDeleteBaseCommandHandler(
            IBaseService<TEntity> service,
            ILogger<SoftDeleteBaseCommandHandler<TEntity>> logger)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result> Handle(
            SoftDeleteBaseCommand<TEntity> request, CancellationToken ct)
        {
            return await _service.SoftDeleteAsync(request.Id, ct);
        }
    }
}
