namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The AI Response feedback domain model.
/// </summary>
public class AIResponseFeedbackDomain
{
	/// <summary>
	/// Gets or sets a value indicating whether this instance is negative feedback.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is negative feedback; otherwise, <c>false</c>.
	/// </value>
	public bool IsNegativeFeedback { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is positive feedback.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is positive feedback; otherwise, <c>false</c>.
	/// </value>
	public bool IsPositiveFeedback { get; set; }

	/// <summary>
	/// Gets or sets the feedback comments.
	/// </summary>
	/// <value>
	/// The feedback comments.
	/// </value>
	public string? FeedbackComments { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the user query.
	/// </summary>
	/// <value>
	/// The user query.
	/// </value>
	public string UserQuery { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the ai response.
	/// </summary>
	/// <value>
	/// The ai response.
	/// </value>
	public string AIResponse { get; set; } = string.Empty;
}
