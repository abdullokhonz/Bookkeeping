using AutoMapper;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookCategoryDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.ReferenceBooks
{
    public class ReferenceBookCategoryProfile : Profile
    {
        public ReferenceBookCategoryProfile()
        {
            //
            // CREATE:ReferenceBookCategoryCreateDto → ReferenceBookCategory
            //
            CreateMap<ReferenceBookCategoryCreateDto, ReferenceBookCategory>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.IfrsAccount, opt => opt.Ignore());

            //
            // UPDATE: ReferenceBookCategoryUpdateDto → ReferenceBookCategory
            //
            CreateMap<ReferenceBookCategoryUpdateDto, ReferenceBookCategory>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.IfrsAccount, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: ReferenceBookCategory → ReferenceBookCategoryGetDto
            //
            CreateMap<ReferenceBookCategory, ReferenceBookCategoryGetDto>();
        }
    }
}
