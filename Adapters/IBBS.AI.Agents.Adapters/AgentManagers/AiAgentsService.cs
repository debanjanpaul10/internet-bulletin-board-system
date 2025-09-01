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
}
