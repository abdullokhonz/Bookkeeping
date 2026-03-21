using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Base;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Interfaces.Base;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Services.Implementations.Base
{
    public class BaseService<T> : IBaseService<T> where T : BaseEntity
    {
        protected readonly IConfiguration _config;
        protected readonly PostgreSQLDbContext _context;
        protected readonly IPostgreSQLRepository<T> _repository;
        protected readonly ILogger<BaseService<T>> _logger;

        public BaseService(
            IPostgreSQLRepository<T> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<BaseService<T>> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public virtual async Task<Result<IEnumerable<T>>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                var all = await _repository.GetAllAsync(ct);
                var active = all.Where(e => !e.IsDeleted).ToList();
                return active;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при GetAll: {Message}", ex.Message);
                throw;
            }
        }

        public virtual async Task<Result<T>> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id, ct);

                if (entity == null || entity.IsDeleted)
                {
                    _logger.LogInformation("GetById: запись не найдена или удалена, Id = {Id}", id);
                    return Result<T>.Failure(Error.NotFound(typeof(T).Name, id));
                }

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при GetById {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public virtual async Task<Result<T>> CreateAsync(T item, CancellationToken ct = default)
        {
            if (item == null) return Result<T>.Failure(Error.NullValue);

            try
            {
                var result = await _repository.CreateAsync(item, ct);
                return result;
            }
            catch (DbUpdateException dbEx)
            {
                _logger.LogWarning(dbEx, "Ошибка уникальности или связей при Create: {Message}", dbEx.Message);

                // Проверяем, является ли это ошибкой уникального индекса (Postgres код 23505)
                if (dbEx.InnerException?.Message.Contains("23505") == true ||
                    dbEx.InnerException?.Message.Contains("unique constraint") == true)
                {
                    // Возвращаем отказ вместо падения. 
                    // Используй подходящий объект Error (например, Error.Conflict или создай новый)
                    return Result<T>.Failure(new Error("Database.DuplicateEntry", "Запись с такими данными уже существует."));
                }

                // Если это другая ошибка БД, которую мы не ожидали
                return Result<T>.Failure(new Error("Database.Error", "Ошибка при сохранении в базу данных."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Непредвиденная ошибка при Create: {Message}", ex.Message);

                // Вместо throw лучше возвращать Failure, чтобы контроллер мог это обработать
                return Result<T>.Failure(new Error("General.Exception", ex.Message));
            }
        }

        public virtual async Task<Result> UpdateAsync(Guid id, T item, CancellationToken ct = default)
        {
            if (item == null) return Result.Failure(Error.NullValue);

            try
            {
                var existing = await _repository.GetByIdAsync(id, ct);
                if (existing == null || existing.IsDeleted)
                {
                    return Result<T>.Failure(Error.NotFound(typeof(T).Name, id));
                }

                var success = await _repository.UpdateAsync(item, ct);
                return success
                    ? Result.Success()
                    : Result.Failure(DomainErrors.General.UpdateFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при Update Id {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public virtual async Task<Result> DeleteAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id, ct);
                if (existing == null)
                {
                    return Result.Failure(Error.NotFound(typeof(T).Name, id));
                }

                var success = await _repository.DeleteAsync(id, ct);
                return success
                    ? Result.Success()
                    : Result.Failure(DomainErrors.General.DeleteFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при Delete Id {Id}: {Message}", id, ex.Message);

                if (ex.InnerException?.Message.Contains("REFERENCE constraint") == true ||
                    ex.Message.Contains("foreign key constraint"))
                {
                    return Result.Failure(new Error("Database.Conflict", "Запись нельзя удалить, так как она используется в других частях системы."));
                }

                return Result.Failure(new Error("Database.Exception", ex.Message));
            }
        }

        public virtual async Task<Result> DeleteAsyncv2(Guid id, CancellationToken ct = default)
        {
            try
            {
                var existing = await _repository.GetByIdAsync(id, ct);
                if (existing == null)
                {
                    return Result<T>.Failure(Error.NotFound(typeof(T).Name, id));
                }

                var success = await _repository.DeleteAsync(id, ct);
                return success
                    ? Result.Success()
                    : Result.Failure(DomainErrors.General.DeleteFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при Delete Id {Id}: {Message}", id, ex.Message);
                throw;
            }
        }

        public virtual async Task<Result> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(id, ct);

                if (entity == null)
                    return Result<T>.Failure(Error.NotFound(typeof(T).Name, id));

                if (entity.IsDeleted)
                    return Result.Success();

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.UtcNow;

                var success = await _repository.UpdateAsync(entity, ct);
                return success
                    ? Result.Success()
                    : Result.Failure(DomainErrors.General.SoftDeleteFailed);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при Soft Delete Id = {Id}", id);
                throw;
            }
        }

        public virtual async Task<Result<PagedList<T>>> GetPagedAsync(int page, int size, CancellationToken ct = default)
        {
            try
            {
                var (items, totalCount) = await _repository.GetPagedAsync(page, size, ct);

                var pagedList = new PagedList<T>(
                    items.Where(e => !e.IsDeleted).ToList().AsReadOnly(),
                    totalCount,
                    page,
                    size
                );

                return pagedList;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пагинации {EntityType}: {Message}", typeof(T).Name, ex.Message);
                throw;
            }
        }
    }
}
