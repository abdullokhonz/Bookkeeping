namespace Bookkeeping.Contracts.Common.Responses
{
    public class ApiResponse<T>
    {
        public Guid RequestId { get; set; }

        public bool IsSuccess { get; set; }

        public string Message { get; set; } = string.Empty;

        public T? Data { get; set; }

        public int? Count { get; set; }

        public PaginationMetadata? Metadata { get; set; }

        public static ApiResponse<T> Success(T data, string message = "Success", PaginationMetadata? metadata = null)
        {
            return new ApiResponse<T>
            {
                RequestId = Guid.NewGuid(),
                IsSuccess = true,
                Message = message,
                Data = data,
                Metadata = metadata,
                Count = data is System.Collections.IEnumerable enumerable
                        ? enumerable.Cast<object>().Count()
                        : (data != null ? 1 : 0)
            };
        }

        public static ApiResponse<T> Fail(string code, string message)
        {
            return new ApiResponse<T>
            {
                RequestId = Guid.NewGuid(),
                IsSuccess = false,
                Message = $"{code}: {message}",
                Data = default
            };
        }
    }
}
