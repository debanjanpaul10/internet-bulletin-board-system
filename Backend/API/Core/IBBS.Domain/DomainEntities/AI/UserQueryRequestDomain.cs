namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The Chat Message Request domain.
/// </summary>
public class UserQueryRequestDomain
{
	/// <summary>
	/// Gets or sets the chat message.
	/// </summary>
	/// <value>
	/// The chat message.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;
}

