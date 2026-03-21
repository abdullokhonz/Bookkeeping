using AutoMapper;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.CashReceiptOrderDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.CashReceiptOrders
{
    public class CashReceiptOrderProfile : Profile
    {
        public CashReceiptOrderProfile()
        {
            //
            // CREATE: CashReceiptOrderCreateDto → CashReceiptOrder
            //
            CreateMap<CashReceiptOrderCreateDto, CashReceiptOrder>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.DocumentNumber, opt => opt.Ignore())
                .ForMember(dest => dest.SequenceNumber, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentYear, opt => opt.Ignore())
                .ForMember(dest => dest.OperationDate, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore())
                .ForMember(dest => dest.DebitIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.DebitIfrsAccount, opt => opt.Ignore())
                .ForMember(dest => dest.CreditIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.CreditIfrsAccount, opt => opt.Ignore())
                .ForMember(dest => dest.IncomeCategory, opt => opt.Ignore())
                .ForMember(dest => dest.ReferenceBook, opt => opt.Ignore())
                .ForMember(dest => dest.VatTax, opt => opt.Ignore());

            //
            // UPDATE: CashReceiptOrderUpdateDto → CashReceiptOrder
            //
            CreateMap<CashReceiptOrderUpdateDto, CashReceiptOrder>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.DocumentNumber, opt => opt.Ignore())
                .ForMember(dest => dest.SequenceNumber, opt => opt.Ignore())
                .ForMember(dest => dest.DocumentYear, opt => opt.Ignore())
                .ForMember(dest => dest.OperationDate, opt => opt.Ignore())
                .ForMember(dest => dest.DebitIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.DebitIfrsAccount, opt => opt.Ignore())
                .ForMember(dest => dest.CreditIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.CreditIfrsAccount, opt => opt.Ignore())
                .ForMember(dest => dest.IncomeCategory, opt => opt.Ignore())
                .ForMember(dest => dest.ReferenceBook, opt => opt.Ignore())
                .ForMember(dest => dest.VatTax, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: CashReceiptOrder → CashReceiptOrderTreeDto
            //
            CreateMap<CashReceiptOrder, CashReceiptOrderGetDto>();
        }
    }
}
