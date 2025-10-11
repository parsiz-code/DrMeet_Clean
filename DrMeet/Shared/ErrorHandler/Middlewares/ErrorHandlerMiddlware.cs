using System.Text.Json;
using DrMeet.Api.Shared.Exceptions;

namespace DrMeet.Api.Shared.ErrorHandler.Middlewares;

public class ErrorHanderMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHanderMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
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

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = new ApiResponse(new string[] { "" });

        switch (ex)
        {
            case BadRequestException re:
                response.Errors = new[] { ex.Message };
                break;
            case ValidationErrorException:
                response.Errors = ex.Message.Split("|");
                break;
            case Exception e:

                //ToDo Complete In Feature
                throw e;

        }

        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(response, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        });
        context.Response.StatusCode = response.StatusCode;
        await context.Response.WriteAsync(result);
    }
}


