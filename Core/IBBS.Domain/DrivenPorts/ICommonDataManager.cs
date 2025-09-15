using IBBS.Domain.DomainEntities;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The common data manager service.
/// </summary>
public interface ICommonDataManager
{
	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>The json format of the sql response.</returns>
	Task<string> ExecuteAISQLQueryAsync(string aiSqlQuery);

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDomain"/></returns>
	Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync();

	/// <summary>
	/// Submits the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportModel">The add bug report model.</param>
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> SubmitBugReportDataAsync(BugReportDomain newBugReportData);

	/// <summary>
	/// Gets the lookup master data async.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDomain"/></returns>
	Task<IEnumerable<LookupMasterDomain>> GetLookupMasterDataAsync();
}
