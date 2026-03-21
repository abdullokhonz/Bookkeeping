using AutoMapper;
using Bookkeeping.Contracts.DTOs.CashReceiptOrders.ImageDto;

using Bookkeeping.Entities.CashReceiptOrders;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.CashReceiptOrders
{
    public class ImageProfile : Profile
    {
        public ImageProfile()
        {
            // --------------------------------------------------------
            // CREATE: ImageCreateDto → Image
            // --------------------------------------------------------
            CreateMap<ImageCreateDto, Image>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Path, opt => opt.Ignore());


            // --------------------------------------------------------
            // UPDATE: ImageUpdateDto → Image
            // --------------------------------------------------------
            CreateMap<ImageUpdateDto, Image>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.Path, opt => opt.Ignore())
                // Если в DTO пришло null, не меняем значение в базе
                .ForAllMembers(opt =>
                    opt.Condition((src, dest, srcMember) =>
                        srcMember != null)
                );


            // --------------------------------------------------------
            // READ: Image → ImageGetDto
            // --------------------------------------------------------
            CreateMap<Image, ImageGetDto>();
        }
    }
}
