namespace Bookkeeping.Contracts.Common.Results
{
    public record Error(string Code, string Message)
    {
        // Пустая ошибка для успешных результатов
        public static readonly Error None =
            new(string.Empty, string.Empty);

        // Частые ошибки
        public static readonly Error NullValue =
            new("Error.NullValue", "Значение не может быть пустым.");

        // Универсальный метод для "Не найдено"
        public static Error NotFound(string entityName, Guid id) =>
            new($"{entityName}.NotFound", $"Запись {entityName} с Id {id} не найдена.");

        // Универсальный метод для ошибок БД
        public static Error Failure(string code, string message) => new(code, message);
    }
}
