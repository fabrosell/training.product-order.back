
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ProductsOrder.Api.Models.Exceptions;


namespace ProductsOrder.Api.Extensions
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            var problemDetails = new
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "An unexpected error occurred.",
                Detail = (string?)null,
                Instance = httpContext.Request.Path
            };

            logger.LogError(exception, "An unhandled exception occurred: {Message}", exception.Message);

            if (exception is DuplicatedProductNameException duplicateNameException)
            {
                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                problemDetails = new
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invalid request.",
                    Detail = (string?)duplicateNameException.Message,
                    Instance = httpContext.Request.Path
                };
                await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
                return true;
            }

            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}


