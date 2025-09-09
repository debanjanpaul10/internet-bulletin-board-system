using System.Globalization;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Knowledgebase;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The AI Service.
/// </summary>
/// <param name="logger">The logger service.</param>
/// <param name="aiAgentsService">The ai agent service.</param>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="mongoDbDatabaseManager">The mongo db database manager.</param>
/// <seealso cref="IBBS.Domain.DrivingPorts.IAIService" />
public class AIService(IAiAgentsService aiAgentsService, ILogger<AIService> logger, IMongoDbDatabaseManager mongoDbDatabaseManager, ICommonDataManager commonDataManager) : IAIService
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
	/// Gets the chatbot response asynchronous.
	/// </summary>
	/// <param name="userQueryRequest">The user query request.</param>
	/// <param name="areFollowupQuestionsEnabled">The boolean flag for followup questions.</param>
	/// <returns>
	/// The ai agent response.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<AIChatbotResponseDomain> GetChatbotResponseAsync(UserQueryRequestDomain userQueryRequest, bool areFollowupQuestionsEnabled)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));

			var aiChatbotResponse = new AIChatbotResponseDomain();
			var userIntent = await aiAgentsService.DetectUserIntentAsync(userQueryRequest).ConfigureAwait(false);
			if (string.IsNullOrEmpty(userIntent))
			{
				throw new Exception(ExceptionConstants.SomethingWentWrongMessage);
			}

			var normalizedIntent = userIntent.Trim().ToUpperInvariant();
			var aiResponse = normalizedIntent switch
			{
				IntentConstants.GreetingIntent => await aiAgentsService.HandleUserGreetingIntentAsync().ConfigureAwait(false),
				IntentConstants.SQLIntent => await InvokeSqlFunctionAsync(userQueryRequest.UserQuery, aiChatbotResponse).ConfigureAwait(false),
				IntentConstants.RAGIntent => await InvokeRAGFunctionAsync(userQueryRequest.UserQuery).ConfigureAwait(false),
				IntentConstants.UnclearIntent => "Cannot determine the user intent",
				_ => string.Empty
			};

			aiChatbotResponse.PrepareAgentChatbotReponse(userIntent.Trim(), userQueryRequest.UserQuery, aiResponse);
			if (areFollowupQuestionsEnabled && (normalizedIntent != IntentConstants.GreetingIntent && normalizedIntent != IntentConstants.UnclearIntent))
			{
				await HandleFollowupQuestionsDataAsync(aiChatbotResponse).ConfigureAwait(false);
			}

			return aiChatbotResponse;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetChatbotResponseAsync), DateTime.UtcNow, userQueryRequest.UserQuery));
		}
	}

	/// <summary>
	/// Posts the ai result feedback asynchronous.
	/// </summary>
	/// <param name="aiResponseFeedback">The ai response feedback.</param>
	/// <param name="userEmail">The user email address.</param>
	/// <returns>
	/// The boolean for success/failure.
	/// </returns>
	public async Task<bool> PostAiResultFeedbackAsync(AIResponseFeedbackDomain aiResponseFeedback, string userEmail)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, userEmail));
			await Task.Delay(300);
			return true;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(PostAiResultFeedbackAsync), DateTime.UtcNow, userEmail));
		}
	}

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>
	/// The list of <see cref="LookupMasterDomain" />
	/// </returns>
	public async Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync()
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
			return await commonDataManager.GetSamplePromptsForChatbotAsync().ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
		}
	}

	#region PRIVATE METHODS

	/// <summary>
	/// Invokes the SQL function asynchronous.
	/// </summary>
	/// <param name="userInput">The user input.</param>
	/// <param name="aiChatbotResponse">The ai chatbot response.</param>
	/// <returns>The AI response data.</returns>
	private async Task<string> InvokeSqlFunctionAsync(string userInput, AIChatbotResponseDomain aiChatbotResponse)
	{
		var databaseSchemaTask = mongoDbDatabaseManager.GetDataFromCollectionAsync<DatabaseSchemaDomain>(
			MongoDBConstants.IbbsKnowledgebaseDB, MongoDBConstants.IBBSDatabaseSchemaCollection);
		var databaseKnowledgeBaseTask = mongoDbDatabaseManager.GetDataFromCollectionAsync<DatabaseKnowledgebaseDomain>(
			MongoDBConstants.IbbsKnowledgebaseDB, MongoDBConstants.IBBSDatabaseKnowledgeBaseCollection);
		await Task.WhenAll(databaseSchemaTask, databaseKnowledgeBaseTask).ConfigureAwait(false);

		var nltosqlInput = new NltosqlInputDomain()
		{
			DatabaseSchema = JsonConvert.SerializeObject(databaseSchemaTask.Result),
			KnowledgeBase = JsonConvert.SerializeObject(databaseKnowledgeBaseTask.Result),
			Source = ConfigurationConstants.SourceName,
			UserQuery = userInput
		};

		var sqlQuery = await aiAgentsService.HandleNLToSQLResponseAsync(nltosqlInput).ConfigureAwait(false);
		var trimmedQuery = sqlQuery.Replace("```sql", string.Empty).Replace("```", string.Empty).Replace("\n", " ").Trim();
		var jsonQuery = await commonDataManager.ExecuteAISQLQueryAsync(trimmedQuery).ConfigureAwait(false);

		var sqlQueryResult = new SqlQueryResult() { JsonQuery = jsonQuery };
		aiChatbotResponse.SqlQuery = trimmedQuery;
		return await aiAgentsService.GetSQLQueryMarkdownResponseAsync(sqlQueryResult).ConfigureAwait(false);
	}

	/// <summary>
	/// Invokes the rag function asynchronous.
	/// </summary>
	/// <param name="userInput">The user input.</param>
	/// <returns>The AI response data.</returns>
	private async Task<string> InvokeRAGFunctionAsync(string userInput)
	{
		var knowledgeBase = await mongoDbDatabaseManager.GetDataFromCollectionAsync<RAGKnowledgebaseDomain>(
			MongoDBConstants.IbbsKnowledgebaseDB, MongoDBConstants.IBBSRAGKnowledgeBaseCollection).ConfigureAwait(false);
		var skillsInput = new SkillsInputDomain()
		{
			KnowledgeBase = JsonConvert.SerializeObject(knowledgeBase.KnowledgeBase),
			Source = ConfigurationConstants.SourceName,
			UserQuery = userInput
		};

		return await aiAgentsService.HandleRAGTextResponseAsync(skillsInput).ConfigureAwait(false);
	}

	/// <summary>
	/// Handles the followup questions data async.
	/// </summary>
	/// <param name="aiResult">The ai result.</param>
	/// <returns>A task to wait on.</returns>
	private async Task HandleFollowupQuestionsDataAsync(AIChatbotResponseDomain aiResult)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.InvariantCulture, LoggingConstants.MethodStartedMessageConstant, nameof(HandleFollowupQuestionsDataAsync), DateTime.UtcNow, aiResult.AIResponseData));
			var followupQuestionsDataDomain = new FollowupQuestionsRequestDomain
			{
				AiResponseData = aiResult.UserIntent == IntentConstants.RAGIntent ? aiResult.AIResponseData.Replace("`", "'") : aiResult.AIResponseData,
				UserIntent = aiResult.UserIntent,
				UserQuery = aiResult.UserQuery
			};

			aiResult.FollowupQuestions = await aiAgentsService.GetFollowupQuestionsResponseAsync(followupQuestionsDataDomain).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.InvariantCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(HandleFollowupQuestionsDataAsync), DateTime.UtcNow, ex.Message));
			aiResult.FollowupQuestions = [];
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.InvariantCulture, LoggingConstants.MethodEndedMessageConstant, nameof(HandleFollowupQuestionsDataAsync), DateTime.UtcNow, aiResult.AIResponseData));
		}
	}

	#endregion
}
