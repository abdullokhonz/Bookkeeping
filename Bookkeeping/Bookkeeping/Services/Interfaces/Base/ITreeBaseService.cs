using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Services.Interfaces.Base
{
    public interface ITreeBaseService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity, ITreeEntity<TEntity>
    {
        Task<Result<IEnumerable<TEntity>>> GetTreeAsync(CancellationToken ct = default);

        Task<Result> RemoveRecursiveAsync(Guid id, CancellationToken ct = default);
    }
}
