using System.Text.Json;

namespace CrudAPI.Util;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (AppException ex)
        {
            _logger.LogError(ex, "Erro esperado: {Message}", ex.Message);
            await HandleExceptionAsync(context, new AppError(ex.StatusCode, ex.Message));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro esperado: {Message}", ex.Message);
            await HandleExceptionAsync(context, new AppError(500,"Erro interno no servidor."));
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, AppError error)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)error.StatusCode;
        return context.Response.WriteAsync(JsonSerializer.Serialize(error));
    }
}

