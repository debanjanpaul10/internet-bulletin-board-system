﻿using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The AI Services Handler interface.
/// </summary>
public interface IAiServicesHandler
{
	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The AI response data</returns>
	Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO);

	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The genre tag response.</returns>
	Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO);

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="userName">The current user name.</param>
	/// <param name="requestDTO">The story.</param>
	/// <returns>The moderation content response.</returns>
	Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO);

	/// <summary>
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="chatMessageRequest">The user query request.</param>
	/// <returns>The ai agent response.</returns>
	Task<AIChatbotResponseDTO> GetChatbotResponseAsync(UserQueryRequestDTO chatMessageRequest);

	/// <summary>
	/// Posts the ai result feedback asynchronous.
	/// </summary>
	/// <param name="aiResponseFeedback">The ai response feedback.</param>
	/// <param name="userEmail">The user email.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> PostAiResultFeedbackAsync(AIResponseFeedbackDTO aiResponseFeedback, string userEmail);

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDTO"/></returns>
	Task<IEnumerable<LookupMasterDTO>> GetSamplePromptsForChatbotAsync();
}
