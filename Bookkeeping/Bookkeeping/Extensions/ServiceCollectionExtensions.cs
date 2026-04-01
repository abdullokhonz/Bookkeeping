using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using Bookkeeping.Infrastructure.Auth;
using Bookkeeping.Infrastructure.Repositories;
using Bookkeeping.Services.Implementations.Accounts5d;
using Bookkeeping.Services.Implementations.Auth;
using Bookkeeping.Services.Implementations.Base;
using Bookkeeping.Services.Implementations.CashReceiptOrders;
using Bookkeeping.Services.Implementations.Notifications;
using Bookkeeping.Services.Implementations.ReferenceBooks;
using Bookkeeping.Services.Implementations.Users;
using Bookkeeping.Services.Interfaces.Accounts5d;
using Bookkeeping.Services.Interfaces.Auth;
using Bookkeeping.Services.Interfaces.Base;
using Bookkeeping.Services.Interfaces.CashReceiptOrders;
using Bookkeeping.Services.Interfaces.Notifications;
using Bookkeeping.Services.Interfaces.ReferenceBooks;
using Bookkeeping.Services.Interfaces.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using MudBlazor.Services;
using Swashbuckle.AspNetCore.Filters;

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

            service.AddScoped<IUserService, UserService>();

            service.AddScoped<IAuthService, AuthService>();

            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<ISmsService, SmsService>();

            service.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            service.AddAutoMapper(cfg =>
            {
                // Здесь можно добавить дополнительные настройки
            }, typeof(Program).Assembly);

            service.AddMudServices();
            service.AddLocalization();
        }

        public static void AddMyAuth(this IServiceCollection services, IConfiguration configuration)
        {
            // Привязываем настройки JWT из appsettings.json к классу AuthOptions
            var jwtSettings = configuration.GetSection("JwtSettings");
            services.Configure<AuthOptions>(jwtSettings);

            var authOptions = jwtSettings.Get<AuthOptions>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions!.Issuer,

                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,

                    ValidateLifetime = true,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey()
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("--- ОШИБКА JWT ---");
                        Console.WriteLine(context.Exception.Message);
                        return Task.CompletedTask;
                    }
                };
            });

            services.AddAuthorizationBuilder()
                .AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"))
                .AddPolicy("UserOnly", policy => policy.RequireRole("User"));

            /* Для более старой версии ASP.NET Core,
             * которая не поддерживает AddAuthorizationBuilder,
             * нужно использовать следующий код:
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
                options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
            });
            */
        }

        public static void AddMySwagger(this IServiceCollection services)
        {
            // Настройка версионирования
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            }).AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

            services.AddSwaggerGen(options =>
            {
                // Получаем IApiVersionDescriptionProvider уже после регистрации сервисов
                var provider = services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new OpenApiInfo
                        {
                            Title = "Bookkeeping API",
                            Version = description.GroupName,
                            Description = description.IsDeprecated ? "Это устаревшая версия API" : "Актуальная версия API"
                        }
                    );
                }

                // Добавляем JWT с помощью фильтра
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Введите токен JWT в формате: Bearer {token}"
                });

                // Используем фильтр для глобального требования авторизации
                options.OperationFilter<SecurityRequirementsOperationFilter>();



                options.EnableAnnotations();
            });
        }
    }
}
