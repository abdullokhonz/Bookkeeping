using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Base;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Services.Implementations.Base
{
    public class TreeBaseService<TEntity> : BaseService<TEntity>, ITreeBaseService<TEntity>
        where TEntity : BaseEntity, ITreeEntity<TEntity>
    {
        public TreeBaseService(
          IPostgreSQLRepository<TEntity> repository,
          IConfiguration config,
          PostgreSQLDbContext context,
          ILogger<TreeBaseService<TEntity>> logger)
          : base(repository, config, context, logger)
        {
        }

        public virtual async Task<Result<IEnumerable<TEntity>>> GetTreeAsync(CancellationToken ct = default)
        {
            var all = await _context.Set<TEntity>().ToListAsync(ct);

            var lookup = all.ToDictionary(c => c.Id);
            var roots = new List<TEntity>();

            foreach (var item in all)
            {
                if (item.ParentId == null)
                {
                    roots.Add(item);
                }
                else
                {
                    if (lookup.TryGetValue(item.ParentId.Value, out var parent))
                    {
                        parent.Children ??= new List<TEntity>();
                        parent.Children.Add(item);
                    }
                }
            }

            return roots;
        }

        public virtual async Task<Result> RemoveRecursiveAsync(Guid id, CancellationToken ct = default)
        {
            var entity = await _context.Set<TEntity>()
              .Include(x => x.Children)
              .FirstOrDefaultAsync(c => c.Id == id, ct);

            if (entity == null)
                return Result<TEntity>.Failure(Error.NotFound(typeof(TEntity).Name, id));

            RemoveRecursive(entity);
            await _context.SaveChangesAsync(ct);

            return Result.Success();
        }

        private void RemoveRecursive(TEntity entity)
        {
            if (entity.Children != null)
            {
                foreach (var child in entity.Children.ToList())
                {
                    RemoveRecursive(child);
                }
            }
            _context.Set<TEntity>().Remove(entity);
        }
    }
}
