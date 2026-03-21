using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Services.Interfaces.Base;
using MediatR;

namespace Bookkeeping.Application.Commands.Base.RemoveTreeBase
{
    public class RemoveTreeBaseCommandHandler<TEntity>
        : IRequestHandler<RemoveTreeBaseCommand<TEntity>, Result>
        where TEntity : BaseEntity, ITreeEntity<TEntity>
    {
        private readonly ITreeBaseService<TEntity> _treeService;
        private readonly ILogger<RemoveTreeBaseCommandHandler<TEntity>> _logger;

        public RemoveTreeBaseCommandHandler(
            ITreeBaseService<TEntity> treeService,
            ILogger<RemoveTreeBaseCommandHandler<TEntity>> logger)
        {
            _treeService = treeService;
            _logger = logger;
        }

        public virtual async Task<Result> Handle(
            RemoveTreeBaseCommand<TEntity> request, CancellationToken ct)
        {
            return await _treeService.RemoveRecursiveAsync(request.Id, ct);
        }
    }
}
