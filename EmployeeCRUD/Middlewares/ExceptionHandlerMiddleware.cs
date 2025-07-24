using Microsoft.AspNetCore.Http;

namespace EmployeeCRUD.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        public ExceptionHandlerMiddleware(RequestDelegate _next, ILogger<ExceptionHandlerMiddleware> _logger)
        {
            this._next = _next;
            this._logger = _logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(ex, $"{errorId}:{ex.Message}");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
