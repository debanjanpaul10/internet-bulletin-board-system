// *********************************************************************************
//	<copyright file="AIService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>AI Service class.</summary>
// *********************************************************************************

namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Data.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.AI;
	using InternetBulletin.Shared.Helpers;
	using Newtonsoft.Json;

	/// <summary>
	/// The AI Service class.
	/// </summary>
	/// <param name="aiDataService">The AI Data service</param>
	/// <param name="httpClientHelper">The http client helper</param>
	/// <seealso cref="IAIService"/>
	public class AIService(IHttpClientHelper httpClientHelper, IAIDataService aiDataService) : IAIService
	{
		/// <summary>
		/// The http client helper.
		/// </summary>
		private readonly IHttpClientHelper _httpClientHelper = httpClientHelper;

		/// <summary>
		/// The AI data service.
		/// </summary>
		private readonly IAIDataService _aiDataService = aiDataService;

		/// <summary>
		/// Rewrites with ai async.
		/// </summary>
		/// <param name="story">The story.</param>
		/// <returns>The AI rewritten response.</returns>
		public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var rewriteAiUrl = RouteConstants.RewriteTextApi_Route;
			var aiStoryResponse = await this.ProcessAIRequestAsync<RewriteResponseDTO>(requestDTO, rewriteAiUrl, userName, AiUsages.RewriteStory);
			return aiStoryResponse.RewrittenStory;
		}

		/// <summary>
		/// Generates the tag for story asynchronous.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The genre tag response.</returns>
		public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var generateTagUrl = RouteConstants.GenerateTagApi_Route;
			var aiResponse = await this.ProcessAIRequestAsync<TagResponseDTO>(requestDTO, generateTagUrl, userName, AiUsages.GenreTag);
			return aiResponse.UserStoryTag;
		}

		/// <summary>
		/// Moderates the content data asynchronous.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="requestDTO">The story.</param>
		/// <returns>The moderation content response.</returns>
		public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDTO requestDTO)
		{
			var moderateContentUrl = RouteConstants.ModerateContentApi_Route;
			var aiResponse = await this.ProcessAIRequestAsync<ModerationContentResponseDTO>(requestDTO, moderateContentUrl, userName, AiUsages.ModerateContent);
			return aiResponse.ContentRating;
		}

		#region PRIVATE Methods

		/// <summary>
		/// Processes an AI request and handles the response.
		/// </summary>
		/// <typeparam name="T">The type of response DTO.</typeparam>
		/// <param name="requestDTO">The request data.</param>
		/// <param name="apiUrl">The API URL.</param>
		/// <param name="userName">The current user name.</param>
		/// <param name="aiUsage">The type of AI usage.</param>
		/// <returns>The processed AI response.</returns>
		private async Task<T> ProcessAIRequestAsync<T>(UserStoryRequestDTO requestDTO, string apiUrl, string userName, AiUsages aiUsage) where T : class
		{
			var response = await this._httpClientHelper.GetIbbsAiResponseAsync(data: requestDTO, apiUrl: apiUrl);
			var aiResponse = JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync()) ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
			await this.SaveAiUsageDataAsync(userName, GetTotalTokensConsumed(aiResponse), GetCandidatesTokenCount(aiResponse), GetPromptTokenCount(aiResponse), nameof(aiUsage));

			return aiResponse;
		}

		/// <summary>
		/// Gets the total tokens consumed from the AI response.
		/// </summary>
		private static int GetTotalTokensConsumed<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.TotalTokensConsumed,
				TagResponseDTO t => t.TotalTokensConsumed,
				ModerationContentResponseDTO m => m.TotalTokensConsumed,
				_ => 0
			};
		}

		/// <summary>
		/// Gets the candidates token count from the AI response.
		/// </summary>
		private static int GetCandidatesTokenCount<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.CandidatesTokenCount,
				TagResponseDTO t => t.CandidatesTokenCount,
				ModerationContentResponseDTO m => m.CandidatesTokenCount,
				_ => 0
			};
		}

		/// <summary>
		/// Gets the prompt token count from the AI response.
		/// </summary>
		private static int GetPromptTokenCount<T>(T response) where T : class
		{
			return response switch
			{
				RewriteResponseDTO r => r.PromptTokenCount,
				TagResponseDTO t => t.PromptTokenCount,
				ModerationContentResponseDTO m => m.PromptTokenCount,
				_ => 0
			};
		}

		/// <summary>
		/// Saves the AI usage data for the current user.
		/// </summary>
		/// <param name="userName">The current user name.</param>
		/// <param name="totalTokensConsumed">Total tokens consumed.</param>
		/// <param name="candidatesTokenCount">Candidates token count.</param>
		/// <param name="promptTokenCount">Prompt token count.</param>
		/// <param name="aiUsage">The type of AI usage.</param>
		/// <returns>A boolean for success/failure.</returns>
		private async Task<bool> SaveAiUsageDataAsync(string userName, int totalTokensConsumed, int candidatesTokenCount, int promptTokenCount, string aiUsage)
		{
			var aiUsageData = new AiUsageDTO
			{
				TotalTokensConsumed = totalTokensConsumed,
				CandidatesTokenCount = candidatesTokenCount,
				PromptTokenCount = promptTokenCount,
				UserName = userName,
				Usage = aiUsage,
				UsageTime = DateTime.UtcNow
			};
			return await this._aiDataService.SaveAiUsageDataAsync(aiUsageData);
		}

		#endregion
	}
}
