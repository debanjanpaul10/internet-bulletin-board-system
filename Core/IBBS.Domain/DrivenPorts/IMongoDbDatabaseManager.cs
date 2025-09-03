using IBBS.Domain.DomainEntities.Knowledgebase;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The Mongo DB database manager.
/// </summary>
public interface IMongoDbDatabaseManager
{
	/// <summary>
	/// Gets the database schema json asynchronous.
	/// </summary>
	/// <returns>The database schema domain.</returns>
	Task<DatabaseSchemaDomain> GetDatabaseSchemaJsonAsync();

	/// <summary>
	/// Gets the database knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>The database knowledge base domain.</returns>
	Task<DatabaseKnowledgebaseDomain> GetDatabaseKnowledgePiecesJsonAsync();

	/// <summary>
	/// Gets the rag knowledge pieces json asynchronous.
	/// </summary>
	/// <returns>The RAG knowledge base domain.</returns>
	Task<RAGKnowledgebaseDomain> GetRAGKnowledgePiecesJsonAsync();
}
