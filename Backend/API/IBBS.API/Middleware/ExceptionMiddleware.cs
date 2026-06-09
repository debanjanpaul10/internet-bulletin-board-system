using IBBS.API.Helpers;
using IBBS.Domain.Helpers;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.APIConstants.LoggingConstants;

namespace IBBS.API.Middleware;

/// <summary>
/// The Exception Middleware class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="next">The request delegate.</param>
public class ExceptionMiddleware(
    ILogger<ExceptionMiddleware> logger,
    RequestDelegate next)
{
    /// <summary>
    /// Invokes the specified HTTP context.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task InvokeAsync(
        HttpContext httpContext,
        CancellationToken cancellationToken = default
    )
    {
        try
        {
            await next(httpContext).ConfigureAwait(false);
        }
        catch (UnauthorizedAccessException ex)
        {
            await this.HandleExceptionAsync(
                httpContext,
                ex,
                statusCode: StatusCodes.Status401Unauthorized,
                error: ex.Message.ToString(),
                message: ex.Message,
                cancellationToken
            ).ConfigureAwait(false);
        }
        catch (BadHttpRequestException ex)
        {
            await this.HandleExceptionAsync(
                httpContext,
                ex,
                statusCode: StatusCodes.Status400BadRequest,
                error: ex.Message.ToString(),
                message: ex.Message,
                cancellationToken
            ).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            await this.HandleExceptionAsync(
                httpContext,
                ex,
                statusCode: StatusCodes.Status500InternalServerError,
                error: ex.Message.ToString(),
                message: ex.Message,
                cancellationToken
            ).ConfigureAwait(false);
        }
    }

    /// <summary>
    /// Handles the exception asynchronous.
    /// </summary>
    /// <param name="httpContext">The HTTP context.</param>
    /// <param name="ex">The ex.</param>
    /// <param name="statusCode">The status code.</param>
    /// <param name="error">The error.</param>
    /// <param name="message">The message.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task HandleExceptionAsync(
        HttpContext httpContext,
        Exception ex,
        int statusCode,
        string error,
        string message,
        CancellationToken cancellationToken = default
    )
    {
        // Get correlation ID from HttpContext
        var correlationId = httpContext.Items[CorrelationIdHeader]?.ToString() ?? LogContext.CorrelationId ?? Guid.NewGuid().ToString();

        // Enrich log context with request details using Microsoft's ILogger.BeginScope
        using (logger.BeginScope(new Dictionary<string, object>
        {
            [HeaderLoggingConstants.CorrelationId] = correlationId,
            [HeaderLoggingConstants.RequestPath] = httpContext.Request.Path.ToString(),
            [HeaderLoggingConstants.RequestMethod] = httpContext.Request.Method,
            [HeaderLoggingConstants.StatusCode] = statusCode
        }))
        {
            logger.LogAppError(
                ex,
                UnhandledExceptionMessage,
                correlationId, httpContext.Request.Path, httpContext.Request.Method
            );
        }

        httpContext.Response.ContentType = ConfigurationConstants.ApplicationJsonConstant;
        httpContext.Response.StatusCode = statusCode;
        var errorResponse = new IBBSBusinessException(message, statusCode, error, correlationId);
        await httpContext.Response.WriteAsJsonAsync(
            value: errorResponse,
            cancellationToken
        ).ConfigureAwait(false);
    }
}

/// <summary>
/// The exception middleware extensions class.
/// </summary>
public static class ExceptionMiddlewareExtensions
{
    /// <summary>
    /// Uses the exception middleware.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder) =>
        builder.UseMiddleware<ExceptionMiddleware>();

}