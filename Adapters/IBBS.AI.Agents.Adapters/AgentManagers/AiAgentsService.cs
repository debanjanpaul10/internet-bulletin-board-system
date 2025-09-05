using IBBS.AI.Agents.Adapters.Helpers;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Globalization;
using static IBBS.AI.Agents.Adapters.Helpers.Constants;

namespace IBBS.AI.Agents.Adapters.AgentManagers;

/// <summary>
/// The AI Agents Service.
/// </summary>
/// <param name="httpClientHelper">The http client helper service.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IAiAgentsService" />
public class AiAgentsService(IHttpClientHelper httpClientHelper, ILogger<AiAgentsService> logger) : IAiAgentsService
{
	#region CHATBOT

	/// <summary>
	/// Detects the user intent asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <returns>
	/// The intent string.
	/// </returns>
	public async Task<string> DetectUserIntentAsync(UserQueryRequestDomain userQueryRequest)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(DetectUserIntentAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(userQueryRequest, AIAgentsRoutesConstants.DetectUserIntent_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();
			return CommonUtilities.PrepareAgentStringResponse(responseString);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(DetectUserIntentAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(DetectUserIntentAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
		}
	}

	/// <summary>
	/// Gets the list of followup questions.
	/// </summary>
	/// <param name="followupQuestionsRequestDomain">The followup questions request.</param>
	/// <returns>
	/// The list of followup questions.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<IEnumerable<string>> GetFollowupQuestionsResponseAsync(FollowupQuestionsRequestDomain followupQuestionsRequestDomain)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, followupQuestionsRequestDomain.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(followupQuestionsRequestDomain, AIAgentsRoutesConstants.GetFollowupQuestionsResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();

			var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
			return JsonConvert.DeserializeObject<IEnumerable<string>>(JsonConvert.SerializeObject(aiResponse.ResponseData))
				?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetFollowupQuestionsResponseAsync), DateTime.UtcNow, followupQuestionsRequestDomain.UserQuery));
		}
	}

	/// <summary>
	/// Gets the SQL query markdown response asynchronous.
	/// </summary>
	/// <param name="sqlQueryResult">The SQL query result.</param>
	/// <returns>
	/// The sql markdown response.
	/// </returns>
	public async Task<string> GetSQLQueryMarkdownResponseAsync(SqlQueryResult sqlQueryResult)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(sqlQueryResult, AIAgentsRoutesConstants.GetSQLQueryMarkdownResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();
			return CommonUtilities.PrepareAgentStringResponse(responseString);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetSQLQueryMarkdownResponseAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Handles the nl to SQL response asynchronous.
	/// </summary>
	/// <param name="nltosqlInput">The nltosql input.</param>
	/// <returns>
	/// The ai generated response.
	/// </returns>
	public async Task<string> HandleNLToSQLResponseAsync(NltosqlInputDomain nltosqlInput)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, nltosqlInput.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(nltosqlInput, AIAgentsRoutesConstants.GetNlToSqlResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();
			return CommonUtilities.PrepareAgentStringResponse(responseString);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleNLToSQLResponseAsync), DateTime.UtcNow, nltosqlInput.UserQuery));
		}
	}

	/// <summary>
	/// Handles the rag text response asynchronous.
	/// </summary>
	/// <param name="skillsInputDomain">The skills input domain.</param>
	/// <returns>
	/// The ai generated response.
	/// </returns>
	public async Task<string> HandleRAGTextResponseAsync(SkillsInputDomain skillsInputDomain)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, skillsInputDomain.UserQuery));
			var response = await httpClientHelper.GetAIResponseAsync(skillsInputDomain, AIAgentsRoutesConstants.GetRAGTextResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();
			return CommonUtilities.PrepareAgentStringResponse(responseString);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleRAGTextResponseAsync), DateTime.UtcNow, skillsInputDomain.UserQuery));
		}
	}

	/// <summary>
	/// Handles the user greeting intent asynchronous.
	/// </summary>
	/// <returns>
	/// The greeting from ai agent.
	/// </returns>
	public async Task<string> HandleUserGreetingIntentAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(string.Empty, AIAgentsRoutesConstants.GetUserGreetingResponse_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync();
			return CommonUtilities.PrepareAgentStringResponse(responseString);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(HandleUserGreetingIntentAsync), DateTime.UtcNow, string.Empty));
		}
	}

	#endregion

	/// <summary>
	/// Moderates the content data asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The moderation content response.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<string> ModerateContentDataAsync(UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(ModerateContentDataAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(requestDTO, AIAgentsRoutesConstants.ModerateContent_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
			return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(ModerateContentDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(ModerateContentDataAsync), DateTime.UtcNow, string.Empty));

		}
	}

	/// <summary>
	/// Rewrites with AI asynchronously.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The AI response data
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<string> RewriteWithAIAsync(UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(RewriteWithAIAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(requestDTO, AIAgentsRoutesConstants.RewriteText_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
			return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(RewriteWithAIAsync), DateTime.UtcNow, string.Empty));

		}
	}

	/// <summary>
	/// Generates the tag for story asynchronous.
	/// </summary>
	/// <param name="requestDTO">The story.</param>
	/// <returns>
	/// The genre tag response.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<string> GenerateTagForStoryAsync(UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, string.Empty));
			var response = await httpClientHelper.GetAIResponseAsync(requestDTO, AIAgentsRoutesConstants.GenerateTag_ApiRoute).ConfigureAwait(false);
			var responseString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

			var aiResponse = JsonConvert.DeserializeObject<AIAgentResponse>(responseString) ?? new AIAgentResponse();
			return aiResponse.ResponseData.ToString() ?? throw new Exception(ExceptionConstants.AiServicesCannotBeAvailedExceptionConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, string.Empty));
		}
	}
}
