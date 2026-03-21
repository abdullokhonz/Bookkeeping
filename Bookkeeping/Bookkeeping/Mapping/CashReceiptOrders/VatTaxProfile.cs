using AutoMapper;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.VatTaxDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.CashReceiptOrders
{
    public class VatTaxProfile : Profile
    {
        public VatTaxProfile()
        {
            //
            // CREATE: VatTaxCreateDto → VatTax
            //
            CreateMap<VatTaxCreateDto, VatTax>()
                .IgnoreBaseEntityFields();

            //
            // UPDATE: VatTaxUpdateDto → VatTax
            //
            CreateMap<VatTaxUpdateDto, VatTax>()
                .IgnoreBaseEntityFields()
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: VatTax → VatTaxTreeDto
            //
            CreateMap<VatTax, VatTaxGetDto>();
        }
    }
}
