using System.Text.Json;

namespace DrMeet.Api.Shared.ErrorHandler.Middlewares;

public class ShapeResponserMiddleware
{
    private readonly RequestDelegate _next;

    public ShapeResponserMiddleware(RequestDelegate next)
       => _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        await using var memoryStream = new MemoryStream();
        context.Response.Body = memoryStream;
        await _next(context);
        memoryStream.Seek(0, SeekOrigin.Begin);
        var responseBodyText = await new StreamReader(memoryStream).ReadToEndAsync();
        memoryStream.Seek(0, SeekOrigin.Begin);

        try
        {
            ApiResponse response = JsonSerializer.Deserialize<ApiResponse>(responseBodyText, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
            });

            context.Response.StatusCode = response.StatusCode;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        context.Response.Body = originalBodyStream;
        await context.Response.Body.WriteAsync(memoryStream.ToArray());
    }
}


