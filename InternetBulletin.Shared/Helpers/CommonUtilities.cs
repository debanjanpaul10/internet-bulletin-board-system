// *********************************************************************************
//	<copyright file="CommonUtilities.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Common utilities.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Helpers
{
    using InternetBulletin.Shared.Constants;
    using Microsoft.Extensions.Logging;

    /// <summary>
    /// Common utilities.
    /// </summary>
    public static class CommonUtilities
    {
        /// <summary>
        /// Throws if null.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <param name="message">The message.</param>
        /// <param name="commonLogger">The common logger.</param>
        /// <typeparam name="T">The null variable type</typeparam>
        /// <typeparam name="L">The logger type.</typeparam>
        public static T ThrowIfNull<T, L>(T obj, string message, ILogger<L> commonLogger)
        {
            if (obj is null)
            {
                ThrowLoggedException(message, commonLogger);
            }
            return obj;
        }

        /// <summary>
        /// Throws logged exception.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="commonLogger">The common logger.</param>
        /// <typeparam name="T"></typeparam>
        public static void ThrowLoggedException<T>(string message, ILogger<T> commonLogger)
        {
            var exception = new Exception(message);
            commonLogger.LogError(exception, exception.Message);
            throw exception;
        }

        /// <summary>
        /// Validates and parse post id.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="logger">The logger.</param>
        /// <typeparam name="T">The logger type.</typeparam>
        public static Guid ValidateAndParsePostId<T>(string postId, ILogger<T> logger)
        {
            if (string.IsNullOrWhiteSpace(postId))
            {
                ThrowLoggedException(ExceptionConstants.PostIdNotPresentMessageConstant, logger);
            }

            if (!Guid.TryParse(postId, out var postGuid))
            {
                ThrowLoggedException(ExceptionConstants.PostGuidNotValidMessageConstant, logger);
            }

            return postGuid;
        }

    }
}


