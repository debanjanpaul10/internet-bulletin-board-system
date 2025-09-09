namespace IBBS.Domain.DomainEntities;

/// <summary>
/// The Lookup master domain entity class.
/// </summary>
public class LookupMasterDomain : BaseEntity
{
	/// <summary>
	/// Gets or sets the identifier.
	/// </summary>
	/// <value>
	/// The identifier.
	/// </value>
	public int Id { get; set; }

	/// <summary>
	/// Gets or sets the type.
	/// </summary>
	/// <value>
	/// The type.
	/// </value>
	public string Type { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the key.
	/// </summary>
	/// <value>
	/// The name of the key.
	/// </value>
	public string KeyName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the key value.
	/// </summary>
	/// <value>
	/// The key value.
	/// </value>
	public string KeyValue { get; set; } = string.Empty;
}
