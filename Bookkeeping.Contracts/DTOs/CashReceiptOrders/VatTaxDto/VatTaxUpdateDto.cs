using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto
{
    public class VatTaxUpdateDto
    {
        [Required(ErrorMessage = "Введите размер ставки НДС")]
        [Range(0, 100, ErrorMessage = "Ставка НДС должна быть от 0 до 100")]
        public decimal? VatRate { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }
    }
}
