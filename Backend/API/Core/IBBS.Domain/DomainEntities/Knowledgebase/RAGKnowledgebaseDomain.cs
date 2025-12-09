using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IBBS.Domain.DomainEntities.Knowledgebase;

/// <summary>
/// The RAG Knowledgebase Domain model.
/// </summary>
[BsonIgnoreExtraElements]
public class RAGKnowledgebaseDomain
{
	/// <summary>
	/// The Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name.
	/// </summary>
	/// <value>
	/// The name.
	/// </value>
	[BsonElement("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the knowledge base.
	/// </summary>
	/// <value>
	/// The knowledge base.
	/// </value>
	[BsonElement("knowledge_base")]
	public IEnumerable<string> KnowledgeBase { get; set; } = [];
}
