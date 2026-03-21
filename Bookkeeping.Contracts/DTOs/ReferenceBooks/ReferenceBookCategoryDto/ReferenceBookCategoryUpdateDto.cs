using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto
{
    public class ReferenceBookCategoryUpdateDto
    {
        [Required(ErrorMessage = "Название категории обязательно")]
        [MaxLength(250, ErrorMessage = "Название не должно превышать 250 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Выберите счёт")]
        public Guid? IfrsAccountId { get; set; }
    }
}
