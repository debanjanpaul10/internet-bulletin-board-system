using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The AI agents service interface.
/// </summary>
public interface IAiAgentsService
{
    /// <summary>
    /// Invoke the workspace agent with user message and get the response.
    /// </summary>
    /// <param name="chatRequestModel">The chat request model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The string response from AI.</returns>
    Task<string> InvokeWorkspaceAgentAsync(
        WorkspaceAgentChatRequestDomain chatRequestModel,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the workspace group chat response.
    /// </summary>
    /// <param name="chatRequestModel">The workspace agent chat request dto model.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The group chat response.</returns>
    Task<string> GetWorkspaceGroupChatResponseAsync(
        WorkspaceAgentChatRequestDomain chatRequestModel,
        CancellationToken cancellationToken = default
    );
}
