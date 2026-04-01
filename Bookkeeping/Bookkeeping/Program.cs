using Asp.Versioning.ApiExplorer;
using Bookkeeping.Components;
using Bookkeeping.Extensions;
using Bookkeeping.Infrastructure.Data;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

var connectionString = builder.Configuration.GetConnectionString("DbPostgres")
    ?? throw new InvalidOperationException("Connection string 'DbPostgres' not found.");

var dataSourseBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourseBuilder.EnableDynamicJson();
dataSourseBuilder.EnableUnmappedTypes();
var dataSource = dataSourseBuilder.Build();

builder.Services.AddDbContext<PostgreSQLDbContext>(options =>
{
    options.UseNpgsql(dataSource)
           .LogTo(Console.WriteLine, LogLevel.Information)
           .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

builder.Services.AddMyServices();

builder.Services.AddMyAuth(builder.Configuration);

long fileSizeLimit = builder.Configuration.GetValue<long>("FileSizeLimit");

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxRequestBodySize = fileSizeLimit;
});

builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = fileSizeLimit;
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
        options.JsonSerializerOptions.WriteIndented = true;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", corsBuilder =>
    {
        corsBuilder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMySwagger();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

// Получаем базовый адрес из appsettings.json
var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"];

// Регистрируем HttpClient
builder.Services.AddScoped(sp =>
{
    var client = new HttpClient();

    if (!string.IsNullOrEmpty(apiBaseUrl))
    {
        client.BaseAddress = new Uri(apiBaseUrl);
    }

    client.DefaultRequestHeaders.Accept.Add(
        new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("*/*"));

    return client;
});

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

var app = builder.Build();

var webRootPath = app.Environment.WebRootPath
    ?? Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
var uploadsPath = Path.Combine(webRootPath, "uploads", "images");

if (!Directory.Exists(uploadsPath))
{
    Directory.CreateDirectory(uploadsPath);
}

app.UseStaticFiles();

app.UseRouting();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();

    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in provider.ApiVersionDescriptions.Reverse())
        {
            options.SwaggerEndpoint(
                $"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant()
            );
        }

        options.RoutePrefix = "swagger";
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), appBuilder =>
{
    appBuilder.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
});

app.UseAutoMapperValidation();

// app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.UseCors("AllowLocalhost");

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapControllers();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Bookkeeping.Client._Imports).Assembly);

app.Run();
