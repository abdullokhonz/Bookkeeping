using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Models;
using Bookkeeping.Entities.Users;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.Users;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Services.Implementations.Users
{
    public class UserService : BaseService<User>, IUserService
    {
        public UserService(
            IPostgreSQLRepository<User> repository,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<UserService> logger)
            : base(repository, config, context, logger)
        {
        }

        public override async Task<Result<User>> GetByIdAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var entity = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, ct);

                if (entity == null)
                    return Result<User>.Failure(Error.NotFound("User", id));

                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при GetById {Id}", id);
                throw;
            }
        }

        public override async Task<Result<IEnumerable<User>>> GetAllAsync(CancellationToken ct = default)
        {
            try
            {
                var users = await _context.Users
                    .Include(u => u.Profile)
                    .Where(u => !u.IsDeleted)
                    .ToListAsync(ct);

                return users;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при GetAllUsers");
                throw;
            }
        }

        public override async Task<Result<PagedList<User>>> GetPagedAsync(int page, int size, CancellationToken ct = default)
        {
            try
            {
                var query = _context.Users
                    .Include(u => u.Profile)
                    .Where(u => !u.IsDeleted);

                var totalCount = await query.CountAsync(ct);
                var items = await query
                    .Skip((page - 1) * size)
                    .Take(size)
                    .ToListAsync(ct);

                return new PagedList<User>(items.AsReadOnly(), totalCount, page, size);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при пагинации пользователей");
                throw;
            }
        }

        public override async Task<Result<User>> CreateAsync(User item, CancellationToken ct = default)
        {
            if (item.Profile == null)
                return Result<User>.Failure(new Error("User.ProfileRequired", "Профиль обязателен"));

            var result = await base.CreateAsync(item, ct);
            if (result.IsFailure) return result;

            // Возвращаем с подгруженным профилем для DTO
            return (await GetByIdAsync(result.Value.Id, ct)).Value;
        }

        public override async Task<Result> UpdateAsync(Guid id, User item, CancellationToken ct = default)
        {
            try
            {
                var existingUser = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted, ct);

                if (existingUser == null)
                    return Result.Failure(Error.NotFound("User", id));

                // Обновляем основные поля юзера
                existingUser.Username = item.Username;
                existingUser.Email = item.Email;
                existingUser.PhoneNumber = item.PhoneNumber;
                existingUser.UpdatedAt = DateTime.UtcNow;

                // Обновляем поля профиля (теперь все, что есть в БД)
                if (existingUser.Profile != null && item.Profile != null)
                {
                    existingUser.Profile.FirstName = item.Profile.FirstName;
                    existingUser.Profile.LastName = item.Profile.LastName;
                    existingUser.Profile.MiddleName = item.Profile.MiddleName;
                    existingUser.Profile.DateOfBirth = item.Profile.DateOfBirth;
                    existingUser.Profile.Gender = item.Profile.Gender;
                    existingUser.Profile.Description = item.Profile.Description;
                    existingUser.Profile.Location = item.Profile.Location;
                    existingUser.Profile.UpdatedAt = DateTime.UtcNow;
                }

                _context.Users.Update(existingUser);

                await _context.SaveChangesAsync(ct);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении пользователя {Id}", id);
                return Result.Failure(DomainErrors.General.UpdateFailed);
            }
        }

        public override async Task<Result> SoftDeleteAsync(Guid id, CancellationToken ct = default)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Profile)
                    .FirstOrDefaultAsync(u => u.Id == id, ct);

                if (user == null) return Result.Failure(Error.NotFound("User", id));

                user.IsDeleted = true;
                user.DeletedAt = DateTime.UtcNow;

                if (user.Profile != null)
                {
                    user.Profile.IsDeleted = true;
                    user.Profile.DeletedAt = DateTime.UtcNow;
                }

                await _context.SaveChangesAsync(ct);
                return Result.Success();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при SoftDelete пользователя {Id}", id);
                return Result.Failure(DomainErrors.General.SoftDeleteFailed);
            }
        }
    }
}
