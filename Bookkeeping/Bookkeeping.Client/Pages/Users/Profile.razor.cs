using Bookkeeping.Client.Providers;
using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Contracts.Enums.Users;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Users
{
    public partial class Profile
    {
        private bool _isLoading = true;
        private Guid _userId;
        private UserResponseDto? _user;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var claimsPrincipal = authState.User;

            var idClaim = claimsPrincipal.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                       ?? claimsPrincipal.FindFirst("id")?.Value
                       ?? claimsPrincipal.FindFirst("sub")?.Value;

            if (Guid.TryParse(idClaim, out var parsedId))
            {
                _userId = parsedId;
                await LoadUserData();
            }
            else
            {
                Snackbar.Add("Не удалось определить ID пользователя из токена", Severity.Error);
                _isLoading = false;
            }
        }

        private async Task LoadUserData()
        {
            _isLoading = true;
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<UserResponseDto>>($"api/v1/User/GetById/{_userId}");

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    _user = response.Data;
                }
                else
                {
                    Snackbar.Add(response?.Message ?? "Не удалось загрузить профиль", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка связи с сервером", Severity.Error);
            }
            finally
            {
                _isLoading = false;
            }
        }

        private async Task Logout()
        {
            if (AuthStateProvider is JwtAuthStateProvider jwtProvider)
            {
                jwtProvider.NotifyUserLogout();
                Snackbar.Add("Вы успешно вышли из системы", Severity.Info);
                NavManager.NavigateTo("/login");
            }
        }

        private string TranslateGender(UserGender gender) => gender switch
        {
            UserGender.Male => "Мужской",
            UserGender.Female => "Женский",
            UserGender.Other => "Другое",
            UserGender.Unknown => "Неизвестный",
            _ => "Не указан"
        };

        private string TranslateRole(UserRole role) => role switch
        {
            UserRole.Admin => "Администратор",
            UserRole.Operator => "Оператор",
            UserRole.Accountant => "Бухгалтер",
            UserRole.Support => "Поддержка",
            UserRole.Guest => "Гость",
            _ => role.ToString()
        };

        private string TranslateType(UserType type) => type switch
        {
            UserType.Client => "Клиент",
            UserType.Employee => "Сотрудник",
            _ => type.ToString()
        };
    }
}
