using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models.AI;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivingPorts;
using Microsoft.Extensions.Configuration;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The AI Services API adapter handler.
/// </summary>
/// <param name="aiServices">The ai services.</param>
/// <param name="mapper">The mapper.</param>
/// <param name="configuration">The configuration.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.IAiServicesHandler" />
public class AiServicesHandler(IAIService aiServices, IMapper mapper, IConfiguration configuration) : IAiServicesHandler
{
	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The genre tag response.
	/// </returns>
	public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.GenerateTagForStoryAsync(userName, domainRequest).ConfigureAwait(false);
	}

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="chatMessageRequest">The user query request.</param>
	/// <returns>
	/// The ai agent response.
	/// </returns>
	public async Task<AIChatbotResponseDTO> GetChatbotResponseAsync(UserQueryRequestDTO chatMessageRequest)
	{
		var areFollowupQuestionsEnabled = bool.TryParse(configuration[ConfigurationConstants.AreFollowupQuestionsEnabled], out var parsedValue) && parsedValue;

		var domainInput = mapper.Map<UserQueryRequestDomain>(chatMessageRequest);
		var domainResponse = await aiServices.GetChatbotResponseAsync(domainInput, areFollowupQuestionsEnabled).ConfigureAwait(false);
		return mapper.Map<AIChatbotResponseDTO>(domainResponse);
	}

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The moderation content response.
	/// </returns>
	public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.ModerateContentDataAsync(userName, domainRequest).ConfigureAwait(false);
	}

	/// <summary>
	/// Posts the ai result feedback asynchronous.
	/// </summary>
	/// <param name="aiResponseFeedback">The ai response feedback.</param>
	/// <param name="userEmail">The user email.</param>
	/// <returns>
	/// The boolean for success/failure.
	/// </returns>
	public async Task<bool> PostAiResultFeedbackAsync(AIResponseFeedbackDTO aiResponseFeedback, string userEmail)
	{
		var domainInput = mapper.Map<AIResponseFeedbackDomain>(aiResponseFeedback);
		return await aiServices.PostAiResultFeedbackAsync(domainInput, userEmail).ConfigureAwait(false);
	}

	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The AI response data
	/// </returns>
	public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO)
	{
		var domainRequest = mapper.Map<UserStoryRequestDomain>(requestDTO);
		return await aiServices.RewriteWithAIAsync(userName, domainRequest).ConfigureAwait(false);
	}
}
