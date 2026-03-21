using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.ReferenceBooks;

namespace Bookkeeping.Services.Implementations.ReferenceBooks
{
    public class ReferenceBookService : BaseService<ReferenceBook>, IReferenceBookService
    {
        private readonly IPostgreSQLRepository<ReferenceBookCategory> _repositoryCategory;
        private readonly IPostgreSQLRepository<IfrsAccount> _repositoryAccount;

        public ReferenceBookService(
            IPostgreSQLRepository<ReferenceBook> repository,
            IPostgreSQLRepository<ReferenceBookCategory> repositoryCategory,
            IPostgreSQLRepository<IfrsAccount> repositoryAccount,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<ReferenceBookService> logger)
            : base(repository, config, context, logger)
        {
            _repositoryCategory = repositoryCategory ?? throw new ArgumentNullException(nameof(repositoryCategory));
            _repositoryAccount = repositoryAccount ?? throw new ArgumentNullException(nameof(repositoryAccount));
        }

        public override async Task<Result<ReferenceBook>> CreateAsync(ReferenceBook entity, CancellationToken ct)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                // Проверка категории
                var category = await _repositoryCategory.GetByIdAsync(entity.ReferenceBookCategoryId, ct);
                if (category == null)
                    return Result<ReferenceBook>.Failure(Error.NotFound("ReferenceBookCategory", entity.ReferenceBookCategoryId));

                // Проверка счета категории
                if (category.IfrsAccount == null)
                {
                    category.IfrsAccount = await _repositoryAccount.GetByIdAsync(category.IfrsAccountId, ct);
                }

                if (category.IfrsAccount == null)
                    return Result<ReferenceBook>.Failure(Error.NotFound("IfrsAccount (Category Main)", category.IfrsAccountId));

                // Генерация номера счета
                var nextNumberResult = await GetNextAvailableAccountNumber(
                    category.IfrsAccount.AccountNumber,
                    category.IfrsAccountId,
                    ct);

                if (nextNumberResult.IsFailure) return Result<ReferenceBook>.Failure(nextNumberResult.Error);

                // Создание субсчета
                var subAccount = new IfrsAccount
                {
                    AccountNumber = nextNumberResult.Value,
                    AccountName = entity.Name,
                    ParentId = category.IfrsAccountId,
                    AccountNature = category.IfrsAccount.AccountNature,
                    CategoryAccountId = category.IfrsAccount.CategoryAccountId,
                    IsActive = true
                };

                await _repositoryAccount.CreateAsync(subAccount, ct);
                await _context.SaveChangesAsync(ct);

                // Создание справочника
                entity.SubIfrsAccountId = subAccount.Id;
                var result = await base.CreateAsync(entity, ct);

                if (result.IsFailure)
                {
                    await transaction.RollbackAsync(ct);
                    return result;
                }

                await transaction.CommitAsync(ct);
                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Ошибка при создании справочника");
                return Result<ReferenceBook>.Failure(DomainErrors.General.Unspecified);
            }
        }

        public override async Task<Result> UpdateAsync(Guid id, ReferenceBook entity, CancellationToken ct)
        {
            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var existingBookResult = await base.GetByIdAsync(id, ct);
                if (existingBookResult.IsFailure) return existingBookResult;

                var existingBook = existingBookResult.Value;

                // Если имя изменилось, обновляем имя связанного счета
                if (existingBook.Name != entity.Name)
                {
                    var account = await _repositoryAccount.GetByIdAsync(existingBook.SubIfrsAccountId, ct);
                    if (account != null)
                    {
                        account.AccountName = entity.Name;
                        await _repositoryAccount.UpdateAsync(account, ct);
                    }
                }

                var result = await base.UpdateAsync(id, entity, ct);
                if (result.IsFailure)
                {
                    await transaction.RollbackAsync(ct);
                    return result;
                }

                await transaction.CommitAsync(ct);
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Ошибка при обновлении справочника");
                return Result.Failure(DomainErrors.General.UpdateFailed);
            }
        }

        public override async Task<Result> DeleteAsync(Guid id, CancellationToken ct)
        {
            var bookResult = await base.GetByIdAsync(id, ct);
            if (bookResult.IsFailure) return bookResult;

            var subAccountId = bookResult.Value.SubIfrsAccountId;

            using var transaction = await _context.Database.BeginTransactionAsync(ct);
            try
            {
                var result = await base.DeleteAsync(id, ct);
                if (result.IsFailure) return result;

                if (subAccountId != Guid.Empty)
                {
                    await _repositoryAccount.DeleteAsync(subAccountId, ct);
                }

                await transaction.CommitAsync(ct);
                return Result.Success();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync(ct);
                _logger.LogError(ex, "Ошибка при удалении справочника и его счета");
                return Result.Failure(DomainErrors.General.DeleteFailed);
            }
        }

        private async Task<Result<string>> GetNextAvailableAccountNumber(string parentNumber, Guid parentId, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(parentNumber) || parentNumber.Length < 7)
            {
                return Result<string>.Failure(DomainErrors.General.ValidationError("Длина главного счета должна быть не менее 7 символов"));
            }

            string basePart = parentNumber.Substring(0, 5);
            var subAccounts = await _repositoryAccount.FindAsync(a => a.ParentId == parentId, ct);

            var usedNumbers = subAccounts
                .Select(a =>
                {
                    string lastTwoDigits = a.AccountNumber.Length >= 7
                        ? a.AccountNumber.Substring(5, 2)
                        : "0";
                    return int.TryParse(lastTwoDigits, out var n) ? n : 0;
                })
                .Where(n => n > 0)
                .OrderBy(n => n)
                .ToList();

            int nextNumber = 1;
            foreach (var num in usedNumbers)
            {
                if (num == nextNumber) nextNumber++;
                else break;
            }

            if (nextNumber > 99)
            {
                return Result<string>.Failure(DomainErrors.General.ValidationError("Достигнут лимит субсчетов (99) для данного счета."));
            }

            return Result<string>.Success($"{basePart}{nextNumber:D2}");
        }
    }
}
