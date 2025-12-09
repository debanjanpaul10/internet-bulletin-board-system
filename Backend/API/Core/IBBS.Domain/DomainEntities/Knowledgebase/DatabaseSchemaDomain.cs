using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IBBS.Domain.DomainEntities.Knowledgebase;

/// <summary>
/// The DB Schema Domain model.
/// </summary>
[BsonIgnoreExtraElements]
public class DatabaseSchemaDomain
{
	/// <summary>
	/// MongoDB document Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// Database tables metadata.
	/// </summary>
	[BsonElement("tables")]
	public IEnumerable<TableSchema> Tables { get; set; } = [];

	/// <summary>
	/// Relationships between tables.
	/// </summary>
	[BsonElement("relationships")]
	public IEnumerable<TableRelationship> Relationships { get; set; } = [];

	/// <summary>
	/// Stored procedures and their parameters.
	/// </summary>
	[BsonElement("storedProcedures")]
	public IEnumerable<StoredProcedureSchema> StoredProcedures { get; set; } = [];

	/// <summary>
	/// Table schema definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class TableSchema
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
		/// Gets or sets the description.
		/// </summary>
		/// <value>
		/// The description.
		/// </value>
		[BsonElement("description")]
		public string Description { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the columns.
		/// </summary>
		/// <value>
		/// The columns.
		/// </value>
		[BsonElement("columns")]
		public IEnumerable<ColumnSchema> Columns { get; set; } = [];
	}

	/// <summary>
	/// Column schema definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ColumnSchema
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
		/// Gets or sets a value indicating whether this <see cref="ColumnSchema"/> is nullable.
		/// </summary>
		/// <value>
		///   <c>true</c> if nullable; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("nullable")]
		public bool Nullable { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether [primary key].
		/// </summary>
		/// <value>
		///   <c>true</c> if [primary key]; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("primaryKey")]
		public bool PrimaryKey { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="ColumnSchema"/> is identity.
		/// </summary>
		/// <value>
		///   <c>true</c> if identity; otherwise, <c>false</c>.
		/// </value>
		[BsonElement("identity")]
		public bool Identity { get; set; }

		/// <summary>
		/// Gets or sets the default.
		/// </summary>
		/// <value>
		/// The default.
		/// </value>
		[BsonElement("default")]
		public object? Default { get; set; }

		/// <summary>
		/// Gets or sets the foreign key.
		/// </summary>
		/// <value>
		/// The foreign key.
		/// </value>
		[BsonElement("foreignKey")]
		public ForeignKeyDefinition? ForeignKey { get; set; }
	}

	/// <summary>
	/// Foreign key definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ForeignKeyDefinition
	{
		/// <summary>
		/// Gets or sets the table.
		/// </summary>
		/// <value>
		/// The table.
		/// </value>
		[BsonElement("table")]
		public string Table { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the column.
		/// </summary>
		/// <value>
		/// The column.
		/// </value>
		[BsonElement("column")]
		public string Column { get; set; } = string.Empty;
	}

	/// <summary>
	/// Table relationship definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class TableRelationship
	{
		/// <summary>
		/// Gets or sets from table.
		/// </summary>
		/// <value>
		/// From table.
		/// </value>
		[BsonElement("fromTable")]
		public string FromTable { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets from column.
		/// </summary>
		/// <value>
		/// From column.
		/// </value>
		[BsonElement("fromColumn")]
		public string FromColumn { get; set; } = string.Empty;

		/// <summary>
		/// Converts to table.
		/// </summary>
		/// <value>
		/// To table.
		/// </value>
		[BsonElement("toTable")]
		public string ToTable { get; set; } = string.Empty;

		/// <summary>
		/// Converts to column.
		/// </summary>
		/// <value>
		/// To column.
		/// </value>
		[BsonElement("toColumn")]
		public string ToColumn { get; set; } = string.Empty;

		/// <summary>
		/// Gets or sets the type.
		/// </summary>
		/// <value>
		/// The type.
		/// </value>
		[BsonElement("type")]
		public string Type { get; set; } = string.Empty;
	}

	/// <summary>
	/// Stored procedure schema definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class StoredProcedureSchema
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
		/// Gets or sets the parameters.
		/// </summary>
		/// <value>
		/// The parameters.
		/// </value>
		[BsonElement("parameters")]
		public IEnumerable<ParameterSchema> Parameters { get; set; } = [];
	}

	/// <summary>
	/// Parameter schema definition.
	/// </summary>
	[BsonIgnoreExtraElements]
	public class ParameterSchema
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
}
