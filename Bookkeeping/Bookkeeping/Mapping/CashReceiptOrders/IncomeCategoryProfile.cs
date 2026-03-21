using AutoMapper;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.IncomeCategoryDto;
using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.CashReceiptOrders
{
    public class IncomeCategoryProfile : Profile
    {
        public IncomeCategoryProfile()
        {
            //
            // CREATE: IncomeCategoryCreateDto → IncomeCategory
            //
            CreateMap<IncomeCategoryCreateDto, IncomeCategory>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.IfrsAccount, opt => opt.Ignore());

            //
            // UPDATE: IncomeCategoryUpdateDto → IncomeCategory
            //
            CreateMap<IncomeCategoryUpdateDto, IncomeCategory>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.IfrsAccount, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: IncomeCategory → IncomeCategoryTreeDto
            //
            CreateMap<IncomeCategory, IncomeCategoryGetDto>();
        }
    }
}
