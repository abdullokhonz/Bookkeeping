using AutoMapper;
using Bookkeeping.Entities.Base;

namespace Bookkeeping.Extensions
{
    public static class AutoMapperExtensions
    {
        public static void UseAutoMapperValidation(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();
            var mapper = scope.ServiceProvider.GetRequiredService<IMapper>();

            try
            {
                mapper.ConfigurationProvider.AssertConfigurationIsValid();
                Console.WriteLine("AutoMapper configuration is valid");
            }
            catch (AutoMapperConfigurationException ex)
            {
                Console.WriteLine("AutoMapper configuration error: " + ex.Message);
                throw;
            }
        }

        public static IMappingExpression<TSource, TDest> IgnoreBaseEntityFields<TSource, TDest>(
            this IMappingExpression<TSource, TDest> map)
            where TDest : BaseEntity
        {
            return map
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedBy, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.DeletedAt, opt => opt.Ignore());
        }
    }
}
