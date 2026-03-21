using Bookkeeping.Contracts.Common.Responses;
using System.Diagnostics;
using System.Net;

namespace Bookkeeping.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        [DebuggerStepThrough]
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            // Тут можно прописать логику: если это ошибка БД — пишем красивый текст
            var message = ex.InnerException?.Message.Contains("REFERENCE constraint") == true
                ? "Ошибка удаления: запись используется в других таблицах."
                : "Произошла внутренняя ошибка сервера.";

            var response = ApiResponse<string>.Fail("Middleware.Error", message);
            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
