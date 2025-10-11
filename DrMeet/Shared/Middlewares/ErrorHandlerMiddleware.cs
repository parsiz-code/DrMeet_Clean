using System.ComponentModel;
using System.Text.Json;
using DrMeet.Api.Shared.Exceptions;

namespace DrMeet.Api.Shared.Middlewares {
public class ErrorHandlerMiddleware(RequestDelegate next, IWebHostEnvironment env, ILogger<ErrorHandlerMiddleware> logger) {
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var response = new ApiResponse();

        switch (ex)
        {
            case BadRequestException:
            case WarningException:
               // response.Warnings = [ex.Message];
                break;
            case ValidationErrorException:
                response.Errors = ex.Message.Split("|");
                break;
            
            // case UnauthorizedException:
            //     response.StatusCode = StatusCodes.Status401Unauthorized;
            //     response.Errors = [ex.Message];
            //     break;
            // case NotImplementedException:
            //     response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            //     response.Errors = ["این سرویس موقتا در دسترس نمی باشد"];
            //     break;
            //
            // case DeprecatedException:
            //     response.StatusCode = StatusCodes.Status503ServiceUnavailable;
            //     response.Errors = ["این سرویس دیگر در دسترس نمی باشد از سرویس های جایگزین استفاده کنید"];
            //     break;
            default:
                logger.LogError(ex, ex.Message);
                response.StatusCode = StatusCodes.Status500InternalServerError;
                response.Errors = ["خطایی در سرور رخ داده است !"];
                if (env.EnvironmentName == "Development")
                {
                    response.Message = ex.Message + Environment.NewLine + ex.InnerException?.Message;
                }

                break;
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
}
