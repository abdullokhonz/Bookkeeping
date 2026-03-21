using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto
{
    public class CategoryAccount5dCreateDto
    {
        [Required(ErrorMessage = "Название категории обязательно")]
        [StringLength(250, ErrorMessage = "Название не должно превышать 250 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        public Guid? ParentId { get; set; }
    }
}
