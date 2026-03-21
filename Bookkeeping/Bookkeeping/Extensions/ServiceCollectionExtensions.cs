using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Accounts5d;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Implementations.CashReceiptOrders;
using Bookkeeping.Services.Implementations.ReferenceBooks;
using Bookkeeping.Services.Interfaces.Accounts5d;
using Bookkeeping.Services.Interfaces.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using MudBlazor.Services;

namespace Bookkeeping.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddMyServices(this IServiceCollection service)
        {
            service.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            service.AddScoped(typeof(ITreeBaseService<>), typeof(TreeBaseService<>));
            service.AddScoped(typeof(IPostgreSQLRepository<>), typeof(PostgreSQLRepository<>));

            service.AddScoped<ICategoryAccount5dService, CategoryAccount5dService>();
            service.AddScoped<IIfrsAccountService, IfrsAccountService>();

            service.AddScoped<IIncomeCategoryService, IncomeCategoryService>();

            service.AddScoped<IReferenceBookCategoryService, ReferenceBookCategoryService>();
            service.AddScoped<IReferenceBookService, ReferenceBookService>();

            service.AddScoped<IVatTaxService, VatTaxService>();

            service.AddScoped<IImageService, ImageService>();

            service.AddScoped<ICashReceiptOrderService, CashReceiptOrderService>();

            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            service.AddAutoMapper(cfg =>
            {
                // Здесь можно добавить дополнительные настройки
            }, typeof(Program).Assembly);

            service.AddMudServices();
            service.AddLocalization();
        }
    }
}
