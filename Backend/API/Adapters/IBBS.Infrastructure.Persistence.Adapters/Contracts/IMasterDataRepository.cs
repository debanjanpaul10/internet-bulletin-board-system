using IBBS.Infrastructure.Persistence.Adapters.Models;

namespace IBBS.Infrastructure.Persistence.Adapters.Contracts;

/// <summary>
/// The master data repository interface.
/// </summary>
/// <remarks>This interface defines methods for executing AI SQL queries and retrieving master data from the database.</remarks>
public interface IMasterDataRepository
{
    /// <summary>
    /// Executes the AI SQL query asynchronous.
    /// </summary>
    /// <remarks>This method executes an AI SQL query and returns the result as a string.</remarks>
    /// <param name="aiSqlQuery">The AI SQL query.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The response from sql.</returns>
    Task<string> ExecuteAISQLQueryAsync(
        string aiSqlQuery,
        CancellationToken cancellationToken = default
    );

    /// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterEntity"/></returns>
	Task<IEnumerable<LookupMasterEntity>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the lookup master data async.
    /// </summary>
    /// <returns>The list of <see cref="LookupMasterEntity"/></returns>
    Task<IEnumerable<LookupMasterEntity>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Submits the bug report data asynchronous.
    /// </summary>
    /// <param name="addBugReportModel">The add bug report model.</param>
    /// <returns>The boolean for success/failure.</returns>
    Task<bool> SubmitBugReportDataAsync(
        BugReportEntity newBugReportData,
        CancellationToken cancellationToken = default
    );
}