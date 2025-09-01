using IBBS.Domain.Helpers;
using static IBBS.API.Helpers.APIConstants;

namespace IBBS.API.Middleware;

/// <summary>
/// The Exception Middleware class.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="next">The request delegate.</param>
public class ExceptionMiddleware(ILogger<ExceptionMiddleware> logger, RequestDelegate next)
{
	/// <summary>
	/// Invokes the specified HTTP context.
	/// </summary>
	/// <param name="httpContext">The HTTP context.</param>
	public async Task Invoke(HttpContext httpContext)
	{
		try
		{
			await next(httpContext);
		}
		catch (UnauthorizedAccessException ex)
		{
			await HandleExceptionAsync(httpContext, ex, StatusCodes.Status401Unauthorized, ex.ToString(), ex.Message);
		}
		catch (Exception ex)
		{
			await HandleExceptionAsync(httpContext, ex, StatusCodes.Status500InternalServerError, ex.ToString(), ex.Message);
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
	public async Task HandleExceptionAsync(HttpContext httpContext, Exception ex, int statusCode, string error, string message)
	{
		logger.LogError(ex, string.Format(LoggingConstants.LogHelperMethodFailed, httpContext.Request.Method, DateTime.UtcNow, ex.Message));

		httpContext.Response.ContentType = ConfigurationConstants.ApplicationJsonConstant;
		httpContext.Response.StatusCode = statusCode;

		var errorResponse = new InternetBulletinBusinessException(statusCode, message, error);
		await httpContext.Response.WriteAsJsonAsync(errorResponse);
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
	public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
	{
		return builder.UseMiddleware<ExceptionMiddleware>();
	}
}