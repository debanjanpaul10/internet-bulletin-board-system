// *********************************************************************************
//	<copyright file="LoggingConstants.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Logging Constants Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.Constants
{
	/// <summary>
	/// The Logging Constants Class.
	/// </summary>
	public static class LoggingConstants
	{
		/// <summary>
		/// The log helper method start
		/// </summary>
		public static readonly string LogHelperMethodStart = "{0} started at {1} for {2}";

		/// <summary>
		/// The log helper method ended
		/// </summary>
		public static readonly string LogHelperMethodEnded = "{0} ended at {1} for {2}";

		/// <summary>
		/// The log helper method failed
		/// </summary>
		public static readonly string LogHelperMethodFailed = "{0} failed at {1} with message {2}";

        /// <summary>
        /// The authorization missing message.
        /// </summary>
        public const string AuthorizationMissingMessage = "Authorization header is missing or empty.";

        /// <summary>
        /// The token missing message.
        /// </summary>
        public const string TokenMissingMessage = "Token value is missing or empty.";

        /// <summary>
        /// The application id mismatch message.
        /// </summary>
        public const string ApplicationIdMismatchMessage = "Application ID does not match the configured client ID.";

        /// <summary>
        /// The token expiry missing message.
        /// </summary>
        public const string TokenExpiryMissingMessage = "Token expiration time is invalid or missing.";

        /// <summary>
        /// The token expired message constant.
        /// </summary>
        public const string TokenExpiredMessageConstant = "Token has expired.";

	}
}
