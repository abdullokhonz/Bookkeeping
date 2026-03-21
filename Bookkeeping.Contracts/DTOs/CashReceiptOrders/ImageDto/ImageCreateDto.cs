using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto
{
    public class ImageCreateDto
    {
        [Required(ErrorMessage = "Укажите название файла")]
        [StringLength(250, ErrorMessage = "Название не должно превышать 250 символов")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Привяжите файл к нужной записи")]
        public Guid EntityId { get; set; }
    }
}
