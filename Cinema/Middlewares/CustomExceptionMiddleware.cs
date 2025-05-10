using System.Net;
using System.Text.Json;

namespace Cinema.Middlewares
{
    public class CustomExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionMiddleware> _logger;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context); // Ejecuta el siguiente middleware
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ocurrió una excepción");
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string mensaje = "Error interno del servidor";

            if (exception is InvalidOperationException)
            {
                statusCode = HttpStatusCode.BadRequest;
                mensaje = exception.Message;
            }

            var resultado = JsonSerializer.Serialize(new
            {
                error = mensaje,
                status = (int)statusCode
            });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            return context.Response.WriteAsync(resultado);
        }
    }
}
