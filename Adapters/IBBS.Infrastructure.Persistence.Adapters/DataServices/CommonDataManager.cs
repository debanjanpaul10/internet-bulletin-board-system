using System.Globalization;
using System.Text.Json;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DrivenPorts;
using IBBS.Infrastructure.Persistence.Adapters.Helpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.Persistence.Adapters.DataServices;

/// <summary>
/// The common data manager service.
/// </summary>
/// <param name="dbContext">The database context.</param>
/// <param name="logger">The logger servoce.</param>
/// <seealso cref="Domain.DrivenPorts.ICommonDataManager" />
public class CommonDataManager(ILogger<CommonDataManager> logger, SqlDbContext dbContext) : ICommonDataManager
{
	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>
	/// The json format of the sql response.
	/// </returns>
	public async Task<string> ExecuteAISQLQueryAsync(string aiSqlQuery)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, aiSqlQuery));
			var result = await ExecuteSqlQueryRawAsync<List<Object>>(aiSqlQuery).ConfigureAwait(false);
			return JsonSerializer.Serialize(result);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, aiSqlQuery));
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
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodStart, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
			var result = await dbContext.LookupMaster.Where(x => x.Type == "SamplePrompts" && x.IsActive).ToListAsync().ConfigureAwait(false);
			return result;
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodFailed, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.LogHelperMethodEnded, nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, string.Empty));
		}
	}

	/// <summary>
	/// Executes the SQL query raw asynchronous.
	/// </summary>
	/// <typeparam name="TResponse">The type of the response.</typeparam>
	/// <param name="sqlQuery">The SQL query.</param>
	/// <returns>The response from sql.</returns>
	private async Task<TResponse> ExecuteSqlQueryRawAsync<TResponse>(string sqlQuery)
	{
		using var connection = dbContext.Database.GetDbConnection();
		await connection.OpenAsync();

		using var command = connection.CreateCommand();
		command.CommandText = sqlQuery;

		using var reader = await command.ExecuteReaderAsync();

		if (typeof(TResponse).IsGenericType && typeof(TResponse).GetGenericTypeDefinition() == typeof(List<>))
		{
			var elementType = typeof(TResponse).GetGenericArguments()[0];
			var list = (System.Collections.IList)Activator.CreateInstance<TResponse>()!;

			while (await reader.ReadAsync())
			{
				var item = reader.MapReaderToObjectOrDictionary(elementType);
				list.Add(item);
			}

			return (TResponse)list;
		}
		else
		{
			if (await reader.ReadAsync())
			{
				var result = reader.MapReaderToObjectOrDictionary(typeof(TResponse));
				return (TResponse)result;
			}

			return default!;
		}
	}
}
