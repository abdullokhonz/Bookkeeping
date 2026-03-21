using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;

namespace Bookkeeping.Services.Interfaces.Base
{
    public interface IBaseService<TEntity>
    {
        Task<Result<IEnumerable<TEntity>>> GetAllAsync(CancellationToken ct = default);

        Task<Result<TEntity>> GetByIdAsync(Guid id, CancellationToken ct = default);

        Task<Result<TEntity>> CreateAsync(TEntity item, CancellationToken ct = default);

        Task<Result> UpdateAsync(Guid id, TEntity item, CancellationToken ct = default);

        Task<Result> DeleteAsync(Guid id, CancellationToken ct = default);

        Task<Result> SoftDeleteAsync(Guid id, CancellationToken ct = default);

        Task<Result<PagedList<TEntity>>> GetPagedAsync(int page, int size, CancellationToken ct = default);
    }
}
