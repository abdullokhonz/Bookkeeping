using Bookkeeping.Entities.Base;

namespace Bookkeeping.Entities.Accounts5d
{
    public class CategoryAccount5d : BaseEntity, ITreeEntity<CategoryAccount5d>
    {
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? ParentId { get; set; }
        public CategoryAccount5d? Parent { get; set; }

        public ICollection<CategoryAccount5d> Children { get; set; } = new List<CategoryAccount5d>();
    }
}
