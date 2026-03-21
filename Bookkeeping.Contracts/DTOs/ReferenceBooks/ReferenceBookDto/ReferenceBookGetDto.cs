namespace Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto
{
    public class ReferenceBookGetDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public Guid ReferenceBookCategoryId { get; set; }

        public Guid SubIfrsAccountId { get; set; }

        public Dictionary<string, object> Info { get; set; } = new();

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
