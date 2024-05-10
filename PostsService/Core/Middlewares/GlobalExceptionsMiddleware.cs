namespace PostsService.Core.Middlewares;

public class GlobalExceptionsMiddleware
{
    private readonly ILogger<GlobalExceptionsMiddleware> _logger;
    private readonly RequestDelegate _next;

    public GlobalExceptionsMiddleware(ILogger<GlobalExceptionsMiddleware> logger, RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await CatchException(context, ex);
        }
    }

    public async Task CatchException(HttpContext context, Exception ex)
    {
        _logger.LogError(ex.ToString());

        throw ex;
    }
}


public static class GlobalExceptionsMiddlewareExtensions
{

    public static IApplicationBuilder UseGlobalExceptions(this IApplicationBuilder app)
    {
        return app.UseMiddleware<GlobalExceptionsMiddleware>();
    }
}
