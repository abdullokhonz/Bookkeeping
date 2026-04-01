using Bookkeeping.Client.Handlers;
using Bookkeeping.Client.Providers;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddLocalization();

builder.Services.AddScoped<AuthenticationStateProvider, JwtAuthStateProvider>();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddTransient<JwtHeaderHandler>();

var apiBaseUrl = builder.Configuration["ApiSettings:BaseUrl"]
    ?? builder.HostEnvironment.BaseAddress;
if (!apiBaseUrl.EndsWith("/")) apiBaseUrl += "/";

builder.Services.AddScoped(sp =>
{
    var handler = sp.GetRequiredService<JwtHeaderHandler>();

    handler.InnerHandler = new HttpClientHandler();

    var client = new HttpClient
    {
        BaseAddress = new Uri(apiBaseUrl)
    };

    client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("*/*"));

    return client;
});



await builder.Build().RunAsync();
