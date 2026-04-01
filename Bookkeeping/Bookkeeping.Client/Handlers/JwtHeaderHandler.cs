using Bookkeeping.Contracts.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Configuration;
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

        // Инжектим зависимости
        public JwtHeaderHandler(IJSRuntime jsRuntime, NavigationManager navManager, IConfiguration config)
        {
            _jsRuntime = jsRuntime;
            _navManager = navManager;

            // Получаем базовый URL API, чтобы знать, куда слать запрос на рефреш
            _apiBaseUrl = config["ApiSettings:BaseUrl"] ?? navManager.BaseUri;
            if (!_apiBaseUrl.EndsWith("/")) _apiBaseUrl += "/";
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // 1. Прикрепляем текущий Access токен
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // 2. Отправляем оригинальный запрос
            var response = await base.SendAsync(request, cancellationToken);

            // 3. ЕСЛИ ТОКЕН ПРОТУХ (сервер вернул 401 Unauthorized)
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                // Пытаемся обновить токен
                bool isRefreshed = await TryRefreshTokenAsync();

                if (isRefreshed)
                {
                    // Если успешно, берем НОВЫЙ токен из хранилища
                    token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");

                    // Обновляем заголовок в оригинальном запросе
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    // НЕЗАМЕТНО ПОВТОРЯЕМ оригинальный запрос!
                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    // Если рефреш не удался (протух и рефреш токен), выкидываем на логин
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                    await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");

                    // Перенаправляем на логин и запоминаем, куда юзер шел
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
                    return false;

                // Создаем DTO для отправки на бэкенд (подставь названия полей, которые ждет твой API)
                var requestPayload = new
                {
                    AccessToken = accessToken,
                    RefreshToken = refreshToken
                };

                // Создаем ЧИСТЫЙ HttpClient, чтобы не попасть в бесконечный цикл перехватчика
                using var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };

                var response = await client.PostAsJsonAsync("api/v1/Auth/refresh-token", requestPayload);

                if (response.IsSuccessStatusCode)
                {
                    // Если бэкенд возвращает твой ApiResponse<TokenResponseDto>, распакуй его:
                    // var apiResult = await response.Content.ReadFromJsonAsync<ApiResponse<TokenResponseDto>>();
                    // var result = apiResult?.Data;

                    // Если возвращает сразу TokenResponseDto:
                    var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();

                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.AccessToken);

                        if (!string.IsNullOrEmpty(result.RefreshToken))
                        {
                            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);
                        }
                        return true;
                    }
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
