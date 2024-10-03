using System.Net;
using Hr.LeaveManagement.Api.Models;
using Hr.LeaveManagement.Application.Exceptions;
using Newtonsoft.Json;

namespace Hr.LeaveManagement.Api.Middleware;

public class ExceptionMiddleware(
    RequestDelegate next,
    ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        CustomValidationProblemDetails problem = new();

        switch (exception)
        {
            case BadRequestException badRequestException:
                statusCode = HttpStatusCode.BadRequest;
                problem = new CustomValidationProblemDetails
                {
                    Title = badRequestException.Message,
                    Status = (int)statusCode,
                    Detail = badRequestException.InnerException?.Message,
                    Type = nameof(BadRequestException),
                    Errors = badRequestException.ValidationErrors
                };
                break;
            case NotFoundException NotFound:
                statusCode = HttpStatusCode.NotFound;
                problem = new CustomValidationProblemDetails
                {
                    Title = NotFound.Message,
                    Status = (int)statusCode,
                    Type = nameof(NotFoundException),
                    Detail = NotFound.InnerException?.Message,
                };
                break;
            default:
                problem = new CustomValidationProblemDetails
                {
                    Title = exception.Message,
                    Status = (int)statusCode,
                    Type = nameof(HttpStatusCode.InternalServerError),
                    Detail = exception.StackTrace,
                };
                break;
        }

        httpContext.Response.StatusCode = (int)statusCode;
        var logMessage = JsonConvert.SerializeObject(problem);
        logger.LogError(logMessage);
        await httpContext.Response.WriteAsJsonAsync(problem);
    }
}