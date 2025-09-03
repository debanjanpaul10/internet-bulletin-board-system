namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The NL to SQL input domain.
/// </summary>
/// <seealso cref="SkillsInputDomain" />
public class NltosqlInputDomain : SkillsInputDomain
{
	/// <summary>
	/// Gets or sets the database schema.
	/// </summary>
	/// <value>
	/// The database schema.
	/// </value>
	public string DatabaseSchema { get; set; } = string.Empty;
}
