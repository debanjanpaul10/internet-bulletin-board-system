namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The Rewrite response data domain model.
/// </summary>
public class RewriteResponseDomain
{
	/// <summary>
	/// The rewrittent story.
	/// </summary>
	public string RewrittenStory { get; set; } = string.Empty;

	/// <summary>
	/// The total tokens consumed.
	/// </summary>
	public int TotalTokensConsumed { get; set; }

	/// <summary>
	/// The candidates token count.
	/// </summary>
	public int CandidatesTokenCount { get; set; }

	/// <summary>
	/// The prompt token count.
	/// </summary>
	public int PromptTokenCount { get; set; }
}