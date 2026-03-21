using Bookkeeping.Contracts.Enums;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.Accounts5d
{
    public class IfrsAccount : BaseEntity, ITreeEntity<IfrsAccount>
    {
        // *Номер счёта (код) формат: X.XX.XX (пример)
        public string AccountNumber { get; set; } = string.Empty;

        // *Название счёта - описательное название отчета
        public string AccountName { get; set; } = string.Empty;

        // Описание - опциональное подробное описание
        public string? Description { get; set; }

        // Родительский счёт для иерархической структуры (Composite Pattern)
        public Guid? ParentId { get; set; }
        public IfrsAccount? Parent { get; set; }

        // *Тип счёта (Active, Passive, ActivePassive)
        public AccountNature AccountNature { get; set; }

        // *Внешний ключ к CategoryAccount
        public Guid CategoryAccountId { get; set; }
        public CategoryAccount5d? CategoryAccount { get; set; }

        // *Активный флаг
        public bool IsActive { get; set; }

        // Набор дочерних счётов для иерархии (Compostie Pattern)
        public ICollection<IfrsAccount> Children { get; set; } = new List<IfrsAccount>();
    }
}
