using Bookkeeping.Entities.Base;
using System.Linq.Expressions;

namespace Bookkeeping.Infrastructure.Repositories
{
    public interface IPostgreSQLRepository<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAllAsync(CancellationToken ct, Expression<Func<T, bool>>? filter = null);
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default);
        Task<T> CreateAsync(T item, CancellationToken ct = default);
        Task<bool> UpdateAsync(T entity, CancellationToken ct = default);
        Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);

        Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int page, int size, CancellationToken ct = default);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default);
    }
}
