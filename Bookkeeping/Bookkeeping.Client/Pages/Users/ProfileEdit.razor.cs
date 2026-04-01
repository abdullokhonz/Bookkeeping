using Bookkeeping.Contracts.Common.Responses;
using Bookkeeping.Contracts.DTOs.Users;
using Bookkeeping.Contracts.Enums.Users;
using MudBlazor;
using System.Net.Http.Json;

namespace Bookkeeping.Client.Pages.Users
{
    public partial class ProfileEdit
    {
        private MudForm _form = null!;
        private bool _isLoading = true;
        private bool _isSaving = false;
        private Guid _userId;
        private UserUpdateDto _updateModel = new();

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthStateProvider.GetAuthenticationStateAsync();
            var idClaim = authState.User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value
                       ?? authState.User.FindFirst("id")?.Value
                       ?? authState.User.FindFirst("sub")?.Value;

            if (Guid.TryParse(idClaim, out var parsedId))
            {
                _userId = parsedId;
                await LoadUserData();
            }
            else
            {
                Snackbar.Add("Не удалось определить ID", Severity.Error);
                NavManager.NavigateTo("/profile");
            }
        }

        private async Task LoadUserData()
        {
            try
            {
                var response = await Http.GetFromJsonAsync<ApiResponse<UserResponseDto>>($"api/v1/User/GetById/{_userId}");

                if (response != null && response.IsSuccess && response.Data != null)
                {
                    var user = response.Data;
                    _updateModel = new UserUpdateDto
                    {
                        Username = user.Username,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        MiddleName = user.MiddleName,
                        Description = user.Description,
                        Location = user.Location,
                        DateOfBirth = user.DateOfBirth,
                        Gender = user.Gender == UserGender.Unknown ? null : user.Gender
                    };
                }
                else
                {
                    Snackbar.Add(response?.Message ?? "Ошибка загрузки данных", Severity.Error);
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

        private async Task SaveChanges()
        {
            await _form.ValidateAsync();
            if (!_form.IsValid) return;

            _isSaving = true;
            try
            {
                var dtoToSend = new UserUpdateDto
                {
                    Username = _updateModel.Username,
                    Email = _updateModel.Email,
                    PhoneNumber = _updateModel.PhoneNumber,
                    FirstName = _updateModel.FirstName,
                    LastName = _updateModel.LastName,
                    MiddleName = _updateModel.MiddleName,
                    Description = _updateModel.Description,
                    Location = _updateModel.Location,
                    DateOfBirth = _updateModel.DateOfBirth,
                    Gender = _updateModel.Gender ?? UserGender.Unknown
                };

                var httpResponse = await Http.PutAsJsonAsync($"api/v1/User/Update/{_userId}", dtoToSend);

                var apiResponse = await httpResponse.Content.ReadFromJsonAsync<ApiResponse<UserResponseDto>>();

                if (httpResponse.IsSuccessStatusCode && apiResponse != null && apiResponse.IsSuccess)
                {
                    Snackbar.Add("Профиль успешно обновлен!", Severity.Success);
                    NavManager.NavigateTo("/profile");
                }
                else
                {
                    Snackbar.Add(apiResponse?.Message ?? "Ошибка при сохранении", Severity.Error);
                }
            }
            catch (Exception)
            {
                Snackbar.Add("Ошибка сервера", Severity.Error);
            }
            finally
            {
                _isSaving = false;
            }
        }
    }
}
