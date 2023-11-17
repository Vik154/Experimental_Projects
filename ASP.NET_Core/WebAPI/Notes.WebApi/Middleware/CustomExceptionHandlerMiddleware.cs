using Notes.Application.Common.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Notes.WebApi.Middleware;

public class CustomExceptionHandlerMiddleware {
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task Invoke(HttpContext context) {
		try {
            await _next(context);
		}
		catch (Exception exp) {
            await HandleExceptionAsync(context, exp);
		}
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exp) {
        var code = HttpStatusCode.InternalServerError;
        var result = string.Empty;

        switch (exp) {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = JsonSerializer.Serialize(validationException);
                break;
            case NotFoundException:
                code = HttpStatusCode.NotFound;
                break;
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;

        if (result == string.Empty) {
            result = JsonSerializer.Serialize(new {error = exp.Message});
        }

        return context.Response.WriteAsync(result);
    }
}
