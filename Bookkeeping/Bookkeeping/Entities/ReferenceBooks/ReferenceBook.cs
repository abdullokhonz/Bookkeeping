using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.ReferenceBooks
{
    public class ReferenceBook : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid ReferenceBookCategoryId { get; set; }
        public ReferenceBookCategory? ReferenceBookCategory { get; set; }

        public Guid SubIfrsAccountId { get; set; }
        public IfrsAccount? SubIfrsAccount { get; set; }

        public Dictionary<string, object> Info { get; set; } = new();
    }
}
