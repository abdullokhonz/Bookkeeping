using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto
{
    public class ReferenceBookCreateDto
    {
        [Required(ErrorMessage = "Название справочника обязательно")]
        [MaxLength(250, ErrorMessage = "Название не должно превышать 250 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public Guid? ReferenceBookCategoryId { get; set; }

        public Dictionary<string, object>? Info { get; set; } = new();
    }
}
