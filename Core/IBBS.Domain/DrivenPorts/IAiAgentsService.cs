using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The AI agents service interface.
/// </summary>
public interface IAiAgentsService
{
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
    /// Gets the ai agent response data asynchronous.
    /// </summary>
    /// <param name="chatRequestDomainModel">The chat request domain model.</param>
    /// <returns>The AI response string.</returns>
    Task<string> GetAiAgentResponseDataAsync(ChatRequestDomainModel chatRequestDomainModel);
}
