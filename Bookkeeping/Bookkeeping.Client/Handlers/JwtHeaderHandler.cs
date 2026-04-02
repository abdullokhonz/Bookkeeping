using Bookkeeping.Contracts.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Handlers
{
    public class JwtHeaderHandler : DelegatingHandler
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly NavigationManager _navManager;
        private readonly string _apiBaseUrl;

        public JwtHeaderHandler(IJSRuntime jsRuntime, NavigationManager navManager, IConfiguration config)
        {
            _jsRuntime = jsRuntime;
            _navManager = navManager;

            _apiBaseUrl = config["ApiSettings:BaseUrl"] ?? navManager.BaseUri;
            if (!_apiBaseUrl.EndsWith("/")) _apiBaseUrl += "/";
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                bool isRefreshed = await TryRefreshTokenAsync();

                if (isRefreshed)
                {
                    token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");

                    var currentUri = _navManager.ToBaseRelativePath(_navManager.Uri);
                    _navManager.NavigateTo($"/login?returnUrl=/{currentUri}");
                }
            }

            return response;
        }

        private async Task<bool> TryRefreshTokenAsync()
        {
            try
            {
                var accessToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                var refreshToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken");

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                {
                    return false;
                }

                var requestPayload = new
                {
                    RefreshToken = refreshToken
                };

                using var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };

                var response = await client.PostAsJsonAsync("api/v1/Auth/refresh-token", requestPayload);

                if (response.IsSuccessStatusCode)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    
                    var result = System.Text.Json.JsonSerializer.Deserialize<TokenResponseDto>(
                        jsonString,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.AccessToken);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);

                        return true;
                    }
                }
                else
                {
                    var errorBody = await response.Content.ReadAsStringAsync();Console.WriteLine($"--- [JWT Handler] ОШИБКА СЕРВЕРА при рефреше: {errorBody} ---");
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
