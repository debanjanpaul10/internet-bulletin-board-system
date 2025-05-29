// *********************************************************************************
//	<copyright file="InternetBulletinBusinessException.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Internet bulletin exception helper.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.ExceptionHelpers
{
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Internet bulletin exception helper.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class InternetBulletinBusinessException : Exception
    {
        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        /// <value>
        /// The status code.
        /// </value>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string? ExceptionMessage { get; set; }

        /// <summary>
        /// Gets or sets the details.
        /// </summary>
        /// <value>
        /// The details.
        /// </value>
        public string? Details { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
        /// </summary>
        /// <param name="statusCode">The status code.</param>
        /// <param name="message">The message.</param>
        /// <param name="details">The details.</param>
        public InternetBulletinBusinessException(string? message) : base(message)
        {
            this.ExceptionMessage = message;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InternetBulletinBusinessException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="statusCode">The status code.</param>
        /// <param name="details">The details.</param>
        public InternetBulletinBusinessException(string? message, int statusCode, string? details) : base(message)
        {
            this.ExceptionMessage = message;
            this.StatusCode = statusCode;
            this.Details = details;
        }
    }
}


