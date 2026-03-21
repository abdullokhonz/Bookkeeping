using AutoMapper;
using Bookkeeping.Contracts.DTOs.ReferenceBooks.ReferenceBookDto;
using Bookkeeping.Entities.ReferenceBooks;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.ReferenceBooks
{
    public class ReferenceBookProfile : Profile
    {
        public ReferenceBookProfile()
        {
            //
            // CREATE: ReferenceBookCreateDto → ReferenceBook
            //
            CreateMap<ReferenceBookCreateDto, ReferenceBook>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.ReferenceBookCategory, opt => opt.Ignore())
                .ForMember(dest => dest.SubIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.SubIfrsAccount, opt => opt.Ignore());

            //
            // UPDATE: ReferenceBookUpdateDto → ReferenceBook
            //
            CreateMap<ReferenceBookUpdateDto, ReferenceBook>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.ReferenceBookCategory, opt => opt.Ignore())
                .ForMember(dest => dest.SubIfrsAccountId, opt => opt.Ignore())
                .ForMember(dest => dest.SubIfrsAccount, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: ReferenceBook → ReferenceBookTreeDto
            //
            CreateMap<ReferenceBook, ReferenceBookGetDto>();
        }
    }
}
