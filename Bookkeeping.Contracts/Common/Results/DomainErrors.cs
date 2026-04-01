namespace Bookkeeping.Contracts.Common.Results;

public static class DomainErrors
{
    public static class General
    {
        public static readonly Error Unspecified =
            new("General.Error", "Произошла непредвиденная ошибка.");

        public static readonly Error UpdateFailed =
            new("General.UpdateFailed", "Не удалось обновить запись в базе данных.");

        public static readonly Error DeleteFailed =
            new("General.DeleteFailed", "Не удалось удалить запись.");

        public static readonly Error SoftDeleteFailed =
            new("General.DeleteFailed", "Не удалось выполнить Soft Delete.");

        public static readonly Error EmptyBody =
            new("General.EmptyBody", "Тело запроса пустое");

        // Ошибка валидации (например, для FluentValidation)
        public static Error ValidationError(string message) =>
            new("General.Validation", message);

        // Ошибка конфликта (запись уже есть)
        public static Error AlreadyExists(string entityName, string value) =>
            new($"{entityName}.AlreadyExists", $"{entityName} с параметром '{value}' уже существует.");
    }

    public static class Image
    {
        public static readonly Error UploadFailed =
            new("Image.UploadFailed", "Не удалось загрузить изображение на диск.");

        public static readonly Error InvalidFormat =
            new("Image.InvalidFormat", "Неподдерживаемый формат файла.");

        public static readonly Error EmptyFile =
            new("Image.EmptyFile", "Файл не передан или пуст.");
    }

    public static class Auth
    {
        public static readonly Error InvalidCredentials =
            new("Auth.InvalidCredentials", "Неверный логин, почта, телефон или пароль.");

        public static readonly Error UserNotFound =
            new("Auth.UserNotFound", "Пользователь не найден.");

        public static readonly Error NotConfirmed =
            new("Auth.NotConfirmed", "Аккаунт не подтвержден. Пожалуйста, введите код подтверждения.");

        public static readonly Error Blocked =
            new("Auth.Blocked", "Аккаунт заблокирован. Обратитесь в поддержку.");

        public static readonly Error InvalidToken =
            new("Auth.InvalidToken", "Недействительный или просроченный токен.");

        public static readonly Error InvalidConfirmationCode =
            new("Auth.InvalidConfirmationCode", "Неверный код подтверждения.");
    }
}
