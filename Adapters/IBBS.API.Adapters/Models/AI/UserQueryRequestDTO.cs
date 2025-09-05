namespace IBBS.API.Adapters.Models.AI;

/// <summary>
/// The Chat Message Request DTO.
/// </summary>
public class UserQueryRequestDTO
{
	/// <summary>
	/// Gets or sets the user query.
	/// </summary>
	/// <value>
	/// The chat message.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;
}

