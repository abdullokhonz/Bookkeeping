using AutoMapper;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Entities.Users;
using Bookkeeping.Extensions;

namespace Bookkeeping.Mapping.Users
{
    public class UserProfileMapping : Profile
    {
        public UserProfileMapping()
        {
            // 1. CREATE: RegisterUserDto → User
            CreateMap<RegisterUserDto, User>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore())
                .ForMember(dest => dest.UserRole, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmationCode, opt => opt.Ignore())
                .ForMember(dest => dest.IsConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
                // ВАЖНО: Мапим данные вложенного профиля при создании
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => new UserProfile
                {
                    FirstName = src.FirstName,
                    LastName = src.LastName
                }));

            // 2. UPDATE: UserUpdateDto → User
            CreateMap<UserUpdateDto, User>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
                .ForMember(dest => dest.UserType, opt => opt.Ignore())
                .ForMember(dest => dest.UserRole, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshToken, opt => opt.Ignore())
                .ForMember(dest => dest.RefreshTokenExpiryTime, opt => opt.Ignore())
                .ForMember(dest => dest.ConfirmationCode, opt => opt.Ignore())
                .ForMember(dest => dest.IsConfirmed, opt => opt.Ignore())
                .ForMember(dest => dest.IsPersonalDataAccepted, opt => opt.Ignore())
                .ForMember(dest => dest.IsBlocked, opt => opt.Ignore())
                // Собираем профиль по частям через ForPath
                .ForPath(dest => dest.Profile!.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForPath(dest => dest.Profile!.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForPath(dest => dest.Profile!.MiddleName, opt => opt.MapFrom(src => src.MiddleName))
                .ForPath(dest => dest.Profile!.Description, opt => opt.MapFrom(src => src.Description))
                .ForPath(dest => dest.Profile!.Location, opt => opt.MapFrom(src => src.Location))
                .ForPath(dest => dest.Profile!.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth))
                .ForPath(dest => dest.Profile!.Gender, opt => opt.MapFrom(src => src.Gender));

            // Дополнительный маппинг для обновления самого профиля
            CreateMap<UserUpdateDto, UserProfile>()
                .IgnoreBaseEntityFields()
                .ForMember(dest => dest.UserId, opt => opt.Ignore())
                .ForMember(dest => dest.User, opt => opt.Ignore())
                .ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

            // 3. READ: User → UserResponseDto (Flattening)
            CreateMap<User, UserResponseDto>()
                // Явно указываем, что данные нужно брать из вложенного объекта Profile
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Profile!.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Profile!.LastName))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.Profile!.MiddleName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Profile!.Description))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.Profile!.DateOfBirth))
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Profile!.Gender))
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Profile!.Location));
        }
    }
}
