namespace IBBS.Infrastructure.MongoDB.Adapters.Helpers;

/// <summary>
/// The constants class.
/// </summary>
internal static class Constants
{
	/// <summary>
	/// The Configuration Constants.
	/// </summary>
	internal static class ConfigurationConstants
	{
		/// <summary>
		/// The mongo database connection string constant for ibbs.
		/// </summary>
		internal const string IbbsMongoDbConnectionString = "IbbsMongoDbConnectionString";

		/// <summary>
		/// The ai agents lab mongo connection string
		/// </summary>
		internal const string AiAgentsLabMongoConnectionString = "AiAgentsLab:MongoDbConnectionString";
	}

	// <summary>
	/// The Logging Constants Class.
	/// </summary>
	internal static class LoggingConstants
	{
		/// <summary>
		/// The method started message constant
		/// </summary>
		internal static readonly string MethodStartedMessageConstant = "Method {0} started at {1}";

		/// <summary>
		/// The method ended message constant
		/// </summary>
		internal static readonly string MethodEndedMessageConstant = "Method {0} ended at {1}";

		/// <summary>
		/// The method failed with message constant.
		/// </summary>
		/// <returns>{0} failed at {1} with {2}</returns>
		internal const string MethodFailedWithMessageConstant = "Method {0} failed at {1} with {2}";
	}

	/// <summary>
	/// The exception constants class.
	/// </summary>
	internal static class ExceptionConstants
	{
		/// <summary>
		/// Something went wrong message constant
		/// </summary>
		internal const string SomethingWentWrongMessageConstant = "Oops! Something went wrong while processing this request!";
	}

	/// <summary>
	/// The MongoDB Constants.
	/// </summary>
	internal static class MongoDBConstants
	{
		/// <summary>
		/// The ai agents knowledge base database
		/// </summary>
		internal const string AiAgentsKnowledgeBaseDatabase = "ai-agents-knowledgebase";

		/// <summary>
		/// The mongo database name constant
		/// </summary>
		internal const string IbbsDatabase = "ibbs-mongo-db";

		/// <summary>
		/// The Application Information Collection
		/// </summary>
		internal const string ApplicationInformationCollectionConstant = "ApplicationInformation";

		/// <summary>
		/// The application technologies collection constant.
		/// </summary>
		internal const string ApplicationTechnologiesCollectionConstant = "ApplicationTechnologies";

		/// <summary>
		/// The ibbs database knowledge base collection constant.
		/// </summary>
		internal const string IBBSDatabaseKnowledgeBaseCollection = "IBBSDatabaseKnowledgeBase";

		/// <summary>
		/// The ibbs database schema collection constant.
		/// </summary>
		internal const string IBBSDatabaseSchemaCollection = "IBBSDatabaseSchema";

		/// <summary>
		/// The ibbsrag knowledge base collection
		/// </summary>
		internal const string IBBSRAGKnowledgeBaseCollection = "IBBSRAGKnowledgeBase";
	}
}
