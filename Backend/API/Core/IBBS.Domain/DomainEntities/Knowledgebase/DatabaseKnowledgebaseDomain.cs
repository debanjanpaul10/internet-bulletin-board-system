using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IBBS.Domain.DomainEntities.Knowledgebase;

/// <summary>
/// The Database Knowledgebase Domain model.
/// </summary>
[BsonIgnoreExtraElements]
public class DatabaseKnowledgebaseDomain
{
	/// <summary>
	/// MongoDB document Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Database name (e.g., IBBS).
	/// </summary>
	[BsonElement("database")]
	public string Database { get; set; } = string.Empty;

	/// <summary>
	/// Purpose/description for the knowledge base document.
	/// </summary>
	[BsonElement("purpose")]
	public string Purpose { get; set; } = string.Empty;

	/// <summary>
	/// Tables metadata.
	/// </summary>
	[BsonElement("tables")]
	public IEnumerable<TableDefinition> Tables { get; set; } = [];

	/// <summary>
	/// Relationships between tables (or notes about relationships).
	/// </summary>
	[BsonElement("relationships")]
	public IEnumerable<RelationshipDefinition> Relationships { get; set; } = [];

	/// <summary>
	/// Business rules guidance.
	/// </summary>
	[BsonElement("businessRules")]
	public IEnumerable<string> BusinessRules { get; set; } = [];

	/// <summary>
	/// Stored procedures and their parameters.
	/// </summary>
	[BsonElement("storedProcedures")]
	public IEnumerable<StoredProcedureDefinition> StoredProcedures { get; set; } = [];

	/// <summary>
	/// Common query patterns.
	/// </summary>
	[BsonElement("queryPatterns")]
	public IEnumerable<QueryPatternDefinition> QueryPatterns { get; set; } = [];

	/// <summary>
	/// General hints.
	/// </summary>
	[BsonElement("hints")]
	public IEnumerable<string> Hints { get; set; } = [];

	/// <summary>
	/// Table description.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class TableDefinition
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the purpose.
		/// </summary>
		/// <value>
		/// The purpose.
		/// </value>
		[BsonElement("purpose")]
		public string Purpose { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the primary key.
		/// </summary>
		/// <value>
		/// The primary key.
		/// </value>
		[BsonElement("primaryKey")]
		public string PrimaryKey { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the important columns.
		/// </summary>
		/// <value>
		/// The important columns.
		/// </value>
		[BsonElement("importantColumns")]
		public IEnumerable<ColumnDefinition> ImportantColumns { get; set; } = [];

		/// <summary>
		/// Gets or sets the common filters.
		/// </summary>
		/// <value>
		/// The common filters.
		/// </value>
		[BsonElement("commonFilters")]
		public IEnumerable<string> CommonFilters { get; set; } = [];

		/// <summary>
		/// Gets or sets the example rows.
		/// </summary>
		/// <value>
		/// The example rows.
		/// </value>
		[BsonElement("exampleRows")]
		public IEnumerable<BsonDocument> ExampleRows { get; set; } = [];
	}

	/// <summary>
	/// Column description.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ColumnDefinition
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[BsonElement("type")]
		public string Type { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the notes.
		/// </summary>
		/// <value>
		/// The notes.
		/// </value>
		[BsonElement("notes")]
		public string Notes { get; set; } = string.Empty;
	}

	/// <summary>
	/// Relationship description; fields are optional to support note-only entries.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class RelationshipDefinition
	{
		/// <summary>
		/// Gets or sets from table.
		/// </summary>
		/// <value>
		/// From table.
		/// </value>
		[BsonElement("fromTable")]
		public string? FromTable { get; set; }

		/// <summary>
		/// Gets or sets from column.
		/// </summary>
		/// <value>
		/// From column.
		/// </value>
		[BsonElement("fromColumn")]
		public string? FromColumn { get; set; }

		/// <summary>
		/// Converts to table.
		/// </summary>
		/// <value>
		/// To table.
		/// </value>
		[BsonElement("toTable")]
		public string? ToTable { get; set; }

		/// <summary>
		/// Converts to column.
		/// </summary>
		/// <value>
		/// To column.
		/// </value>
		[BsonElement("toColumn")]
		public string? ToColumn { get; set; }

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[BsonElement("type")]
		public string? Type { get; set; }

		/// <summary>
		/// Gets or sets the note.
		/// </summary>
		/// <value>
		/// The note.
		/// </value>
		[BsonElement("note")]
		public string? Note { get; set; }
	}

	/// <summary>
	/// Stored procedure description.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class StoredProcedureDefinition
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the purpose.
		/// </summary>
		/// <value>
		/// The purpose.
		/// </value>
		[BsonElement("purpose")]
		public string Purpose { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		[BsonElement("parameters")]
		public IEnumerable<ParameterDefinition> Parameters { get; set; } = [];

		/// <summary>
		/// Gets or sets the usage example.
		/// </summary>
		/// <value>
		/// The usage example.
		/// </value>
		[BsonElement("usageExample")]
		public string UsageExample { get; set; } = string.Empty;
	}

	/// <summary>
	/// Stored procedure parameter.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ParameterDefinition
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[BsonElement("type")]
		public string Type { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the default.
		/// </summary>
		/// <value>
		/// The default.
		/// </value>
		[BsonElement("default")]
		public string? Default { get; set; }
	}

	/// <summary>
	/// Query pattern description.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class QueryPatternDefinition
	{
		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		/// <value>
		/// The name.
		/// </value>
		[BsonElement("name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the SQL.
		/// </summary>
		/// <value>
		/// The SQL.
		/// </value>
		[BsonElement("sql")]
		public string Sql { get; set; } = string.Empty;
	}
}
