using Bookkeeping.Entities.Base;
using Bookkeeping.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Bookkeeping.Infrastructure.Repositories
{
    public class PostgreSQLRepository<T> : IPostgreSQLRepository<T> where T : BaseEntity
    {
        readonly PostgreSQLDbContext _context;
        private readonly DbSet<T> _dbSet;
        public PostgreSQLRepository(PostgreSQLDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken ct, Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            return await query.ToListAsync(ct);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            return await _dbSet.FindAsync(new object[] { id }, ct);
        }

        public async Task<T> CreateAsync(T item, CancellationToken ct = default)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            await _dbSet.AddAsync(item, ct);
            await _context.SaveChangesAsync(ct);
            return item;
        }

        public async Task<bool> UpdateAsync(T entity, CancellationToken ct = default)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));

            _dbSet.Update(entity);
            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await GetByIdAsync(id, ct);
            if (entity == null)
                return false;

            _dbSet.Remove(entity);
            return await _context.SaveChangesAsync(ct) > 0;
        }

        public async Task<(IEnumerable<T> Items, int TotalCount)> GetPagedAsync(int page, int size, CancellationToken ct = default)
        {
            if (page <= 0) page = 1;
            if (size <= 0) size = 10;
            if (size > 100) size = 100;

            var query = _dbSet.AsNoTracking().Where(e => !e.IsDeleted);

            var total = await query.CountAsync(ct);
            var items = await query
                .Skip((page - 1) * size)
                .Take(size)
                .ToListAsync(ct);

            return (items, total);
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken ct = default)
        {
            if (predicate == null) throw new ArgumentNullException(nameof(predicate));

            return await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .ToListAsync(ct);
        }
    }
}
