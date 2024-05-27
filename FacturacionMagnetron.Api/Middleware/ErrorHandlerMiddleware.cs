using FacturacionMagnetron.Domain.Dto;
using FacturacionMagnetron.Infrastructure.Extensions;
using System.Net;
using System.Text.Json;


namespace FacturacionMagnetron.Api.Middleware
{
    public class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlerMiddleware> _logger;
        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) {
        
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                Log.Save("Ingreso_ "+ context.GetEndpoint() );
                await next(context);
            }
            catch (Exception error)
            {
              await  HandleExceptionAsync(context, error);
            }
        }
        private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            httpContext.Response.ContentType = "application/json";
            var response = httpContext.Response;
            var errorResponse =  ResponseDto<dynamic>.Failure(exception.Message);
     
            switch (exception)
            {
                case UnauthorizedAccessException:
                    response.StatusCode = (int)HttpStatusCode.Unauthorized; break;
                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            _logger.LogError("Error: { error},", exception.Message);
            Log.Save("Error " + exception.Message);
            var result = JsonSerializer.Serialize(errorResponse, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            await httpContext.Response.WriteAsync(result);
        }
    }
}
