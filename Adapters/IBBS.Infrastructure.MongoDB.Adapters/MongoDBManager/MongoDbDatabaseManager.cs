using IBBS.Domain.DomainEntities.Knowledgebase;
using IBBS.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using System.Globalization;
using static IBBS.Infrastructure.MongoDB.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.MongoDB.Adapters.MongoDBManager;

/// <summary>
/// The MongoDB database manager class.
/// </summary>
/// <param name="mongoClient">The mongo database client.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IMongoDbDatabaseManager" />
public class MongoDbDatabaseManager(IMongoClient mongoClient, ILogger<MongoDbDatabaseManager> logger) : IMongoDbDatabaseManager
{
	/// <summary>
	/// The ai agents database
	/// </summary>
	private readonly IMongoDatabase _aiAgentsDatabase = mongoClient.GetDatabase(MongoDBConstants.AiAgentsKnowledgeBaseDatabase);

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The database knowledge base domain.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<DatabaseKnowledgebaseDomain> GetDatabaseKnowledgePiecesJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow));

			var knowledgePieces = _aiAgentsDatabase.GetCollection<DatabaseKnowledgebaseDomain>(MongoDBConstants.IBBSDatabaseKnowledgeBaseCollection);
			if (knowledgePieces is not null)
			{
				return await knowledgePieces.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseKnowledgePiecesJsonAsync), DateTime.UtcNow));
		}
	}

	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>
	/// The database schema domain.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<DatabaseSchemaDomain> GetDatabaseSchemaJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow));

			var dbSchema = _aiAgentsDatabase.GetCollection<DatabaseSchemaDomain>(MongoDBConstants.IBBSDatabaseSchemaCollection);
			if (dbSchema is not null)
			{
				return await dbSchema.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDatabaseSchemaJsonAsync), DateTime.UtcNow));
		}
	}

	/// <summary>
	/// Gets the rag knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>
	/// The RAG knowledge base domain.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<RAGKnowledgebaseDomain> GetRAGKnowledgePiecesJsonAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow));

			var knowledgePieces = _aiAgentsDatabase.GetCollection<RAGKnowledgebaseDomain>(MongoDBConstants.IBBSRAGKnowledgeBaseCollection);
			if (knowledgePieces is not null)
			{
				return await knowledgePieces.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetRAGKnowledgePiecesJsonAsync), DateTime.UtcNow));
		}
	}
}
