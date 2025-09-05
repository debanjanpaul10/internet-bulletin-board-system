namespace IBBS.Domain.DomainEntities;

/// <summary>
/// The Lookup master domain entity class.
/// </summary>
public class LookupMasterDomain
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
	public string KeyValue {  get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets a value indicating whether this instance is active.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
	/// </value>
	public bool IsActive { get; set; }

	/// <summary>
	/// Gets or sets the date created.
	/// </summary>
	/// <value>
	/// The date created.
	/// </value>
	public DateTime DateCreated { get; set; }

	/// <summary>
	/// Gets or sets the created by.
	/// </summary>
	/// <value>
	/// The created by.
	/// </value>
	public string CreatedBy { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the date modified.
	/// </summary>
	/// <value>
	/// The date modified.
	/// </value>
	public DateTime DateModified { get; set; }

	/// <summary>
	/// Gets or sets the modified by.
	/// </summary>
	/// <value>
	/// The modified by.
	/// </value>
	public string ModifiedBy { get; set; } = string.Empty;
}
