using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace kilozdazolik.Ecommerce.API.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "[ERROR]: {Message}", exception.Message);
        
        (int statusCode, string title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource is not found"),
            
            ArgumentException or ArgumentNullException => (StatusCodes.Status400BadRequest, "Invalid argument"),
            
            InvalidOperationException => (StatusCodes.Status409Conflict, "Already exists"),
            
            _ => (StatusCodes.Status500InternalServerError, "Server error")
        };
        
        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception.Message, 
            Type = exception.GetType().Name
        };

        httpContext.Response.StatusCode = statusCode;
        
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        
        return true;
    }
}