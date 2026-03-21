using Bookkeeping.Contracts.Enums;
using Bookkeeping.Contracts.Interfaces.Components;

namespace Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto
{
    public class IfrsAccountTreeDto : IIfrsAccountComponent
    {
        public Guid Id { get; set; }

        public string AccountNumber { get; set; } = string.Empty;

        public string AccountName { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid? ParentId { get; set; }

        public AccountNature AccountNature { get; set; }

        public Guid CategoryAccountId { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public List<IfrsAccountTreeDto> Children { get; set; } = new();

        public void AddSub(IfrsAccountTreeDto child)
        {
            Children.Add(child);
        }

        public void RemoveSub(IfrsAccountTreeDto child)
        {
            Children.Remove(child);
        }

        public IEnumerable<IfrsAccountTreeDto> GetChildren() => Children;
    }
}
