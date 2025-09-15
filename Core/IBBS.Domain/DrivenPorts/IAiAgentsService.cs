using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The AI agents service interface.
/// </summary>
public interface IAiAgentsService
{
	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The AI response data</returns>
	Task<string> RewriteWithAIAsync(UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The genre tag response.</returns>
	Task<string> GenerateTagForStoryAsync(UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The moderation content response.</returns>
	Task<string> ModerateContentDataAsync(UserStoryRequestDomain requestDTO);

	/// <summary>
	/// Detects the user intent asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>The intent string.</returns>
	Task<string> DetectUserIntentAsync(UserQueryRequestDomain userQueryRequest);

	/// <summary>
	/// Handles the user greeting intent asynchronous.
	/// </summary>
	/// <returns>The greeting from ai agent.</returns>
	Task<string> HandleUserGreetingIntentAsync();

	/// <summary>
	/// Handles the rag text response asynchronous.
	/// </summary>
	/// <param name="skillsInputDomain">The skills input domain.</param>
	/// <returns>The ai generated response.</returns>
	Task<string> HandleRAGTextResponseAsync(SkillsInputDomain skillsInputDomain);

	/// <summary>
	/// Handles the nl to SQL response asynchronous.
	/// </summary>
	/// <param name="nltosqlInput">The nltosql input.</param>
	/// <returns>The ai generated response.</returns>
	Task<string> HandleNLToSQLResponseAsync(NltosqlInputDomain nltosqlInput);

	/// <summary>
	/// Gets the SQL query markdown response asynchronous.
	/// </summary>
	/// <param name="sqlQueryResult">The SQL query result.</param>
	/// <returns>The sql markdown response.</returns>
	Task<string> GetSQLQueryMarkdownResponseAsync(SqlQueryResult sqlQueryResult);

	/// <summary>
	/// Gets the list of followup questions.
	/// </summary>
	/// <param name="followupQuestionsRequestDomain">The followup questions request.</param>
	/// <returns>The list of followup questions.</returns>
	Task<IEnumerable<string>> GetFollowupQuestionsResponseAsync(FollowupQuestionsRequestDomain followupQuestionsRequestDomain);

	/// <summary>
	/// Generate the bug severity using LLM.
	/// </summary>
	/// <param name="bugSeverityAiRequest">The bug severity AI request model.</param>
	/// <returns>The bug severity.</returns>
	Task<string> GenerateBugSeverityAsync(BugSeverityAIRequestDomain bugSeverityAiRequest);
}
