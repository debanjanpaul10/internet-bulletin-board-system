namespace IBBS.Infrastructure.Persistence.Adapters.Helpers;

/// <summary>
/// The constants class.
/// </summary>
internal static class Constants
{
	/// <summary>
	/// The logging constants.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The log helper method start
		/// </summary>
		internal const string LogHelperMethodStart = "{0} started at {1} for {2}";

		/// <summary>
		/// The log helper method ended
		/// </summary>
		internal const string LogHelperMethodEnded = "{0} ended at {1} for {2}";

		/// <summary>
		/// The log helper method failed
		/// </summary>
		internal const string LogHelperMethodFailed = "{0} failed at {1} with message {2}";
	}

	/// <summary>
	/// Configuration constants class.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The SQL connection string constant
		/// </summary>
		internal const string SqlConnectionStringConstant = "SqlConnectionString";

		/// <summary>
		/// The local sql connection string constant.
		/// </summary>
		internal const string LocalSqlConnectionStringConstant = "LocalSqlServerConnection";
	}

	/// <summary>
	/// The exception constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// The post not found message constant
		/// </summary>
		internal const string PostNotFoundMessageConstant = "It seems the post you are looking for does not exists anymore!";

		/// <summary>
		/// The unable to get user post ratings message constant
		/// </summary>
		internal const string UnableToGetUserPostRatingsMessageConstant = "Unable to retrieve user's post ratings as of this moment!";
	}
}
