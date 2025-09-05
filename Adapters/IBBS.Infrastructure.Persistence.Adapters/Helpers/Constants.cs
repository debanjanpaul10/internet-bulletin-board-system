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

		/// <summary>
		/// The post exists message constant
		/// </summary>
		internal const string PostExistsMessageConstant = "A post with the generated ID already exists.";
	}

	/// <summary>
	/// The database constants class.
	/// </summary>
	public static class DatabaseConstants
	{
		/// <summary>
		/// The integer data type constant
		/// </summary>
		public const string IntegerDataTypeConstant = "int";

		/// <summary>
		/// The n variable character maximum data type constant
		/// </summary>
		public const string NVarCharMaxDataTypeConstant = "nvarchar(max)";

		/// <summary>
		/// The bit data type constant
		/// </summary>
		public const string BitDataTypeConstant = "bit";

		/// <summary>
		/// The unique identifier data type constant.
		/// </summary>
		public const string UniqueIdentifierDataTypeConstant = "uniqueidentifier";

		/// <summary>
		/// The date time data type constant.
		/// </summary>
		public const string DateTimeDataTypeConstant = "datetime";

		/// <summary>
		/// The users table constant
		/// </summary>
		public const string UsersTableConstant = "Users";

		/// <summary>
		/// The posts table constant.
		/// </summary>
		public const string PostsTableConstant = "Posts";

		/// <summary>
		/// The primary key posts constant.
		/// </summary>
		public const string PrimaryKeyPostsConstant = "PK_Posts";

		/// <summary>
		/// The post ratings table name constant.
		/// </summary>
		public const string PostRatingsTableNameConstant = "PostRatings";

		/// <summary>
		/// The AI Usages table name constant.
		/// </summary>
		public const string AiUsagesTableNameConstant = "AiUsages";

		/// <summary>
		/// The lookup master table name
		/// </summary>
		internal const string LookupMasterTableName = "LookupMaster";
	}
}
