using Bookkeeping.Contracts.Enums;
using System.ComponentModel.DataAnnotations;

namespace Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto
{
    public class CashReceiptOrderUpdateDto
    {
        [Required(ErrorMessage = "Укажите наименование документа")]
        [StringLength(250, ErrorMessage = "Наименование не должно превышать 250 символов")]
        public string? Name { get; set; }

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Укажите сумму")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Некорректная сумма")]
        public decimal? Amount { get; set; }

        [Required(ErrorMessage = "Выберите статус документа")]
        public DocumentStatus? Status { get; set; }

        [Required(ErrorMessage = "Выберите статью доходов")]
        public Guid? IncomeCategoryId { get; set; }

        [Required(ErrorMessage = "Выберите справочник")]
        public Guid? ReferenceBookId { get; set; }

        public Guid? VatTaxId { get; set; }

        [Required(ErrorMessage = "Укажите ФИО бухгалтера")]
        [StringLength(150, ErrorMessage = "Слишком длинное значение")]
        public string? Accountant { get; set; }

        [Required(ErrorMessage = "Укажите ФИО кассира")]
        [StringLength(150, ErrorMessage = "Слишком длинное значение")]
        public string? Cashier { get; set; }
    }
}
