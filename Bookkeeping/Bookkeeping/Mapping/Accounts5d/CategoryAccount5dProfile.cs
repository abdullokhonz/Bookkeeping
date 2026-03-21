using AutoMapper;
using Bookkeeping.Contracts.DTOs.Accounts5d.CategoryAccount5dDto;
using Bookkeeping.Entities.Accounts5d;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.Accounts5d
{
    public class CategoryAccount5dProfile : Profile
    {
        public CategoryAccount5dProfile()
        {
            //
            // CREATE: CategoryAccount5dCreateDto → CategoryAccount5d
            //
            CreateMap<CategoryAccount5dCreateDto, CategoryAccount5d>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore());

            //
            // UPDATE: CategoryAccount5dUpdateDto → CategoryAccount5d
            //
            CreateMap<CategoryAccount5dUpdateDto, CategoryAccount5d>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Parent, opt => opt.Ignore())
                .ForMember(dest => dest.Children, opt => opt.Ignore())
                // Обновляем только те поля, которые != null
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );

            //
            // READ: CategoryAccount5d → CategoryAccount5dTreeDto
            //
            CreateMap<CategoryAccount5d, CategoryAccount5dTreeDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children))
                // Эта настройка позволяет AutoMapper корректно обрабатывать вложенные списки Children
                .PreserveReferences() // Помогает мапперу узнавать объекты, которые он уже мапил
                                      // Ограничиваем глубину, чтобы случайно не уйти в бесконечный цикл, если в БД будет ошибка связей
                .MaxDepth(10);
        }
    }
}
