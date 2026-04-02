using Bookkeeping.Contracts.Common.Responses;
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
            // Пишем лог КРАСНЫМ цветом (Console.Error), чтобы браузер точно его не скрыл!
            Console.Error.WriteLine($"[ШАГ 1] Отправляем запрос: {request.Method} {request.RequestUri}");

            // 1. Прикрепляем текущий Access токен
            var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            // 2. Отправляем оригинальный запрос на бэкенд
            var response = await base.SendAsync(request, cancellationToken);

            Console.Error.WriteLine($"[ШАГ 2] Получен ответ от {request.RequestUri}. Статус кода: {(int)response.StatusCode} ({response.StatusCode})");

            // 3. ЕСЛИ ТОКЕН ПРОТУХ
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                Console.Error.WriteLine("[ШАГ 3] Сервер вернул 401 Unauthorized! Запускаем TryRefreshTokenAsync()...");

                bool isRefreshed = await TryRefreshTokenAsync();

                if (isRefreshed)
                {
                    Console.Error.WriteLine("[ШАГ 4] Рефреш успешен! Повторяем исходный запрос...");
                    token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    Console.Error.WriteLine("[ШАГ 5] Рефреш провалился (токены стерты). Редирект на логин.");
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
                Console.WriteLine("--- [JWT Handler] Поймали 401. Начинаем процесс обновления токена... ---");

                var accessToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                var refreshToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken");

                if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
                {
                    Console.WriteLine("--- [JWT Handler] ОШИБКА: В localStorage нет токенов! ---");
                    return false;
                }

                Console.WriteLine($"--- [JWT Handler] Токены найдены. Отправляем запрос на {_apiBaseUrl}api/v1/Auth/refresh-token ---");

                var requestPayload = new
                {
                    RefreshToken = refreshToken
                };

                using var client = new HttpClient { BaseAddress = new Uri(_apiBaseUrl) };

                // Отправляем запрос
                var response = await client.PostAsJsonAsync("api/v1/Auth/refresh-token", requestPayload);

                Console.WriteLine($"--- [JWT Handler] Сервер ответил статусом: {response.StatusCode} ---");

                if (response.IsSuccessStatusCode)
                {
                    // Читаем ответ как строку, чтобы сначала проверить, что там пришло
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"--- [JWT Handler] Успешный ответ от сервера: {jsonString} ---");

                    // Парсим в объект
                    var result = System.Text.Json.JsonSerializer.Deserialize<TokenResponseDto>(
                        jsonString,
                        new System.Text.Json.JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.AccessToken);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);

                        Console.WriteLine("--- [JWT Handler] Токены успешно обновлены в localStorage! ---");
                        return true;
                    }
                    else
                    {
                        Console.WriteLine("--- [JWT Handler] ОШИБКА: Сервер вернул 200 OK, но AccessToken пустой или не распарсился! ---");
                    }
                }
                else
                {
                    // Если сервер вернул 400, 401, 500 и т.д. - читаем почему
                    var errorBody = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"--- [JWT Handler] ОШИБКА СЕРВЕРА при рефреше: {errorBody} ---");
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--- [JWT Handler] КРИТИЧЕСКАЯ ОШИБКА в коде рефреша: {ex.Message} ---");
                return false;
            }
        }
    }
}
