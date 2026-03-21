namespace Bookkeeping.Contracts.Common.Results
{
    public class Result
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public Error Error { get; }

        protected Result(bool isSuccess, Error error)
        {
            // нельзя создать успех с ошибкой или провал без ошибки
            if (isSuccess && error != Error.None)
                throw new InvalidOperationException("У успешного результата не может быть ошибки.");

            if (!isSuccess && error == Error.None)
                throw new InvalidOperationException("У провального результата должна быть ошибка.");

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result Success() => new(true, Error.None);

        public static Result Failure(Error error) => new(false, error);
    }

    public class Result<T> : Result
    {
        private readonly T? _value;

        private Result(T? value, bool isSuccess, Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }

        // Защита от попытки взять Value у ошибки
        public T Value => IsSuccess
            ? _value!
            : throw new InvalidOperationException("Нельзя получить значение из неуспешного результата.");

        public static Result<T> Success(T value) => new(value, true, Error.None);

        public static new Result<T> Failure(Error error) => new(default, false, error);

        public TResult Map<TResult>(Func<T, TResult> onSuccess, Func<Error, TResult> onFailure)
        {
            return IsSuccess ? onSuccess(Value) : onFailure(Error);
        }

        // неявное преобразование типа
        // Позволяет писать просто: return myDto;
        // вместо return Result<MyDto>.Success(myDto);
        public static implicit operator Result<T>(T? value) =>
            value is not null ? Success(value) : Failure(Error.None);
    }
}
