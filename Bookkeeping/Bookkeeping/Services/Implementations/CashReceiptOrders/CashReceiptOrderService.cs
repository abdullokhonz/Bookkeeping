using Bookkeeping.Contracts.Common.Results;
using Bookkeeping.Contracts.Enums;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Infrastructure.Data;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using Microsoft.EntityFrameworkCore;

namespace Bookkeeping.Services.Implementations.CashReceiptOrders
{
    public class CashReceiptOrderService
        : BaseService<CashReceiptOrder>, ICashReceiptOrderService
    {
        private new readonly IPostgreSQLRepository<CashReceiptOrder> _repository;
        private readonly IPostgreSQLRepository<ReferenceBook> _repositoryReferenceBook;
        private readonly IPostgreSQLRepository<IfrsAccount> _repositoryIfrsAccount;
        private new readonly PostgreSQLDbContext _context;

        public CashReceiptOrderService(
            IPostgreSQLRepository<CashReceiptOrder> repository,
            IPostgreSQLRepository<ReferenceBook> repositoryReferenceBook,
            IPostgreSQLRepository<IfrsAccount> repositoryIfrsAccount,
            IConfiguration config,
            PostgreSQLDbContext context,
            ILogger<CashReceiptOrderService> logger)
            : base(repository, config, context, logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _repositoryReferenceBook = repositoryReferenceBook ?? throw new ArgumentNullException(nameof(repositoryReferenceBook));
            _repositoryIfrsAccount = repositoryIfrsAccount ?? throw new ArgumentNullException(nameof(repositoryIfrsAccount));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public override async Task<Result<CashReceiptOrder>> CreateAsync(CashReceiptOrder entity, CancellationToken ct)
        {
            var (docNumber, seqNumber, year) = await GenerateDocumentNumberAsync(ct);

            entity.DocumentNumber = docNumber;
            entity.SequenceNumber = seqNumber;
            entity.DocumentYear = year;

            entity.OperationDate = DateTime.UtcNow;

            entity.Status = DocumentStatus.Draft;

            var referenceBook = await _repositoryReferenceBook.GetByIdAsync(entity.ReferenceBookId, ct);
            if (referenceBook == null) throw new Exception("Справочник (поставщик) не найден");
            if (referenceBook.SubIfrsAccountId == Guid.Empty) throw new Exception("У справочника не настроен субсчёт");

            entity.CreditIfrsAccountId = referenceBook.SubIfrsAccountId;
            entity.DebitIfrsAccountId = await GetDefaultCashAccountIdAsync(ct);

            return await base.CreateAsync(entity, ct);
        }

        public override async Task<Result> UpdateAsync(Guid id, CashReceiptOrder entity, CancellationToken ct)
        {
            var existingOrder = await _repository.GetByIdAsync(id, ct);
            if (existingOrder == null)
                return Result<CashReceiptOrder>.Failure(Error.NotFound(entity.Name, id));

            entity.OperationDate = DateTime.UtcNow;

            if (existingOrder.ReferenceBookId != entity.ReferenceBookId)
            {
                var referenceBook = await _repositoryReferenceBook.GetByIdAsync(entity.ReferenceBookId, ct);
                if (referenceBook?.SubIfrsAccountId != null)
                {
                    entity.CreditIfrsAccountId = referenceBook.SubIfrsAccountId;
                }
            }
            else
            {
                entity.CreditIfrsAccountId = existingOrder.CreditIfrsAccountId;
            }

            entity.DebitIfrsAccountId = existingOrder.DebitIfrsAccountId;

            entity.DocumentNumber = existingOrder.DocumentNumber;

            return await base.UpdateAsync(id, entity, ct);
        }

        private async Task<(string DocNumber, int SeqNumber, int Year)> GenerateDocumentNumberAsync(CancellationToken ct)
        {
            var currentYear = DateTime.UtcNow.Year;
            var prefix = $"ПКО-{currentYear}-";

            var maxSequence = await _context.CashReceiptOrders
                .IgnoreQueryFilters()
                .Where(o => o.DocumentYear == currentYear)
                .MaxAsync(o => (int?)o.SequenceNumber, ct) ?? 0;

            int nextSequenceNumber = maxSequence + 1;

            string docNumber = $"{prefix}{nextSequenceNumber}";

            return (docNumber, nextSequenceNumber, currentYear);
        }

        private async Task<Guid> GetDefaultCashAccountIdAsync(CancellationToken ct)
        {
            var cashAccount = await _repositoryIfrsAccount.GetAllAsync(ct, a => a.AccountNumber == "1010000");
            var account = cashAccount.FirstOrDefault();

            if (account == null)
            {
                throw new Exception("Счёт кассы по умолчанию (1010000) не найден в системе.");
            }

            return account.Id;
        }
    }
}
