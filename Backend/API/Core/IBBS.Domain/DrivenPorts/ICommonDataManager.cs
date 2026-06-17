using IBBS.Domain.DomainEntities;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The common data manager service.
/// </summary>
/// <remarks>This interface defines methods for executing AI SQL queries and retrieving master data from the database.</remarks>
public interface ICommonDataManager
{
	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>The json format of the sql response.</returns>
	Task<string> ExecuteAISQLQueryAsync(
		string aiSqlQuery,
		CancellationToken cancellationToken = default
	);

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDomain"/></returns>
	Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync(
		CancellationToken cancellationToken = default
	);

	/// <summary>
	/// Submits the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportModel">The add bug report model.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> SubmitBugReportDataAsync(
		BugReportDomain newBugReportData,
		CancellationToken cancellationToken = default
	);

	/// <summary>
	/// Gets the lookup master data async.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDomain"/></returns>
	Task<IEnumerable<LookupMasterDomain>> GetLookupMasterDataAsync(
		CancellationToken cancellationToken = default
	);
}
