using Bookkeeping.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto
{
    public class IfrsAccountUpdateDto
    {
        // Номер счета обычно ReadOnly, но валидацию лучше оставить
        [Required(ErrorMessage = "Номер счета обязателен")]
        [RegularExpression(@"^\d{7}$", ErrorMessage = "Номер счета должен состоять ровно из 7 цифр")]
        public string? AccountNumber { get; set; }

        [Required(ErrorMessage = "Название счета обязательно")]
        [MaxLength(250, ErrorMessage = "Название не должно превышать 250 символов")]
        public string? AccountName { get; set; }

        [MaxLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        public Guid? ParentId { get; set; }

        [Required(ErrorMessage = "Выберите тип")]
        public AccountNature? AccountNature { get; set; }

        [Required(ErrorMessage = "Выберите категорию")]
        public Guid? CategoryAccountId { get; set; }

        public bool? IsActive { get; set; }
    }
}
