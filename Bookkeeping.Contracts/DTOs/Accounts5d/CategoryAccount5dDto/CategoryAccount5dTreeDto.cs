using Bookkeeping.Contracts.Interfaces.Components;

namespace Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto
{
    public class CategoryAccount5dTreeDto : ICategoryAccount5dComponent
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? ParentId { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<CategoryAccount5dTreeDto> Children { get; set; } = new();

        public void AddSub(CategoryAccount5dTreeDto child)
        {
            Children.Add(child);
        }

        public void RemoveSub(CategoryAccount5dTreeDto child)
        {
            Children.Remove(child);
        }

        public IEnumerable<CategoryAccount5dTreeDto> GetChildren() => Children;
    }
}
