namespace IBBS.Domain.UseCases;

using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using Microsoft.Extensions.Logging;
using System.Globalization;
using static IBBS.Domain.Helpers.DomainConstants;


public class AIService(IAiAgentsService aiAgentsService, ILogger<AIService> logger, IMongoDbDatabaseManager mongoDbDatabaseManager) : IAIService
{
	/// <summary>
	/// Rewrites the provided story using AI processing.
	/// </summary>
	/// <param name="userName">The username of the user requesting the rewrite.</param>
	/// <param name="requestDTO">The story content to be rewritten.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the AI-rewritten story.</returns>
	public async Task<string> RewriteWithAIAsync(string userName, UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, userName));
			return await aiAgentsService.RewriteWithAIAsync(requestDTO).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(RewriteWithAIAsync), DateTime.UtcNow, userName));
		}
	}

	/// <summary>
	/// Generates a genre tag for the provided story using AI processing.
	/// </summary>
	/// <param name="userName">The username of the user requesting the tag generation.</param>
	/// <param name="requestDTO">The story content for which to generate a tag.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the generated genre tag.</returns>
	public async Task<string> GenerateTagForStoryAsync(string userName, UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, userName));
			return await aiAgentsService.GenerateTagForStoryAsync(requestDTO).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GenerateTagForStoryAsync), DateTime.UtcNow, userName));
		}
	}

	/// <summary>
	/// Moderates the content of the provided story using AI processing.
	/// </summary>
	/// <param name="userName">The username of the user requesting content moderation.</param>
	/// <param name="requestDTO">The story content to be moderated.</param>
	/// <returns>A task that represents the asynchronous operation. The task result contains the content rating.</returns>
	public async Task<string> ModerateContentDataAsync(string userName, UserStoryRequestDomain requestDTO)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, userName));
			return await aiAgentsService.ModerateContentDataAsync(requestDTO).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(ModerateContentDataAsync), DateTime.UtcNow, userName));
		}
	}

	/// <summary>
	/// Gets the application information data asynchronously.
	/// </summary>
	/// <returns>
	/// The about us details data <see cref="AboutUsAppInfoDataDomain" />
	/// </returns>
	public async Task<AboutUsAppInfoDataDomain> GetAboutUsDataAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, string.Empty));
			return await mongoDbDatabaseManager.GetAboutUsDataAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetAboutUsDataAsync), DateTime.UtcNow, string.Empty));
		}
	}


}
