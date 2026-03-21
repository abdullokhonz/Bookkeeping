using AutoMapper;
using Bookkeeping.Contracts.DTOs.Accounts5d.IfrsAccountDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.Accounts5d
{
    public class IfrsAccountProfile : Profile
    {
        public IfrsAccountProfile()
        {
            //
            // CREATE: IfrsAccountCreateDto → IfrsAccount
            //
            CreateMap<IfrsAccountCreateDto, IfrsAccount>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryAccount, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            //
            // UPDATE: IfrsAccountUpdateDto → IfrsAccount
            //
            CreateMap<IfrsAccountUpdateDto, IfrsAccount>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.CategoryAccount, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: IfrsAccount → IfrsAccountTreeDto
            //
            CreateMap<IfrsAccount, IfrsAccountTreeDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                // Эта настройка позволяет AutoMapper корректно обрабатывать вложенные списки Children
                .PreserveReferences() // Помогает мапперу узнавать объекты, которые он уже мапил
                // Ограничиваем глубину, чтобы случайно не уйти в бесконечный цикл, если в БД будет ошибка связей
                .MaxDepth(10);
        }
    }
}
