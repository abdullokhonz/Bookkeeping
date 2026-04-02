using Bookkeeping.Contracts.DTOs.Auth;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;

namespace Bookkeeping.Client.Providers
{
    public class JwtAuthStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime _jsRuntime;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly NavigationManager _navManager;

        public JwtAuthStateProvider(
            IJSRuntime jsRuntime,
            HttpClient httpClient,
            IConfiguration config,
            NavigationManager navManager)
        {
            _jsRuntime = jsRuntime;
            _httpClient = httpClient;
            _config = config;
            _navManager = navManager;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            try
            {
                var token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                var refreshToken = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "refreshToken");

                if (string.IsNullOrWhiteSpace(token))
                {
                    return Anonymous();
                }

                var claims = ParseClaimsFromJwt(token);
                var expClaim = claims.FirstOrDefault(c => c.Type == "exp");

                if (expClaim != null)
                {
                    var expTime = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value));

                    if (expTime <= DateTimeOffset.UtcNow.AddMinutes(1))
                    {
                        if (string.IsNullOrWhiteSpace(refreshToken))
                            return Anonymous();

                        var isRefreshed = await RefreshTokenOnStartupAsync(refreshToken);

                        if (isRefreshed)
                        {
                            token = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", "authToken");
                            claims = ParseClaimsFromJwt(token);
                        }
                        else
                        {
                            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
                            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");
                            return Anonymous();
                        }
                    }
                }

                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }
            catch
            {
                return Anonymous();
            }
        }

        private async Task<bool> RefreshTokenOnStartupAsync(string refreshToken)
        {
            try
            {
                var apiBaseUrl = _config["ApiSettings:BaseUrl"] ?? _navManager.BaseUri;
                if (!apiBaseUrl.EndsWith("/")) apiBaseUrl += "/";

                var requestPayload = new RefreshTokenRequestDto { RefreshToken = refreshToken };

                using var client = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };

                var response = await client.PostAsJsonAsync("api/v1/Auth/refresh-token", requestPayload);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TokenResponseDto>();
                    if (result != null && !string.IsNullOrEmpty(result.AccessToken))
                    {
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "authToken", result.AccessToken);
                        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "refreshToken", result.RefreshToken);
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

        private AuthenticationState Anonymous()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        public void NotifyUserAuthentication(string token)
        {
            var claims = ParseClaimsFromJwt(token);
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(claims, "jwt"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public async Task NotifyUserLogout()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "authToken");
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", "refreshToken");

            _httpClient.DefaultRequestHeaders.Authorization = null;

            var authState = Task.FromResult(new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs!.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()!));
        }

        private byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
