using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;
using IBBS.Domain.DrivingPorts;
using static IBBS.API.Adapters.Mapping.DomainToResponseMapper;
using static IBBS.API.Adapters.Mapping.RequestToDomainMapper;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The AI Services API adapter handler.
/// </summary>
/// <param name="aiServices">The ai services.</param>
/// <seealso cref="IAiServicesHandler" />
public sealed class AiServicesHandler(IAIService aiServices) : IAiServicesHandler
{
    /// <inheritdoc/>
    public async Task<string> GenerateBugSeverityAsync(
        BugSeverityAIRequestDTO bugSeverityAiRequest,
        CancellationToken cancellationToken = default
    )
    {
        var domainRequest = MapToDomain(requestDto: bugSeverityAiRequest);
        return await aiServices.GenerateBugSeverityAsync(
            bugSeverityAiRequest: domainRequest,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<string> GenerateTagForStoryAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        var domainRequest = MapToDomain(requestDto: requestDTO);
        return await aiServices.GenerateTagForStoryAsync(
            userName,
            requestDTO: domainRequest,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<string> GetChatbotResponseAsync(
        UserQueryRequestDTO userQueryRequest,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: userQueryRequest);
        return await aiServices.GetChatbotResponseAsync(
            userQueryRequest: domainInput,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<LookupMasterDTO>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await aiServices.GetSamplePromptsForChatbotAsync(
            cancellationToken
        ).ConfigureAwait(false);
        return [.. domainResult.Select(MapToResponse)];
    }

    /// <inheritdoc/>
    public async Task<string> ModerateContentDataAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        var domainRequest = MapToDomain(requestDto: requestDTO);
        return await aiServices.ModerateContentDataAsync(
            userName,
            requestDTO: domainRequest,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<bool> PostAiResultFeedbackAsync(
        AIResponseFeedbackDTO aiResponseFeedback,
        string userEmail,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: aiResponseFeedback);
        return await aiServices.PostAiResultFeedbackAsync(
            aiResponseFeedback: domainInput,
            userEmail,
            cancellationToken
        ).ConfigureAwait(false);
    }

    /// <inheritdoc/>
    public async Task<string> RewriteWithAIAsync(
        string userName,
        UserStoryRequestDTO requestDTO,
        CancellationToken cancellationToken = default
    )
    {
        var domainRequest = MapToDomain(requestDto: requestDTO);
        return await aiServices.RewriteWithAIAsync(
            userName,
            requestDTO: domainRequest,
            cancellationToken
        ).ConfigureAwait(false);
    }
}
