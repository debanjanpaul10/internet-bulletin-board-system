// *********************************************************************************
//	<copyright file="GlobalExceptionHandler.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Global Exception Handler.</summary>
// *********************************************************************************

namespace InternetBulletin.API.Middleware
{
    using System;
    using InternetBulletin.Shared.ExceptionHelpers;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The Global Exception Handler.
    /// </summary>
    /// <param name="logger">The logger</param>
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<GlobalExceptionHandler> _logger = logger;

        /// <summary>
        /// Tries handle async.
        /// </summary>
        /// <param name="httpContext">The http context.</param>
        /// <param name="exception">The exception.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;
            if (exception is InternetBulletinBusinessException ex)
            {
                httpContext.Response.StatusCode = (int)ex.StatusCode;
                problemDetails.Title = ex.Message;
            }
            else
            {
                problemDetails.Title = exception.Message;
            }

            this._logger.LogError(problemDetails.Title);
            problemDetails.Status = httpContext.Response.StatusCode;
            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken).ConfigureAwait(false);

            return true;
        }

    }

}