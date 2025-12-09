namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The SQL Query Result DTO.
/// </summary>
public class SqlQueryResult
{
	/// <summary>
	/// Gets or sets the json query.
	/// </summary>
	/// <value>
	/// The json query.
	/// </value>
	public string JsonQuery { get; set; } = string.Empty;
}