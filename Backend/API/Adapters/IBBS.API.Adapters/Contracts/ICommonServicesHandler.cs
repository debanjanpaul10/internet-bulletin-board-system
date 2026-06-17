using IBBS.API.Adapters.Models;

namespace IBBS.API.Adapters.Contracts;

/// <summary>
/// The Common services adapter handler interface.
/// </summary>
public interface ICommonServicesHandler
{
    /// <summary>
    /// Submits the bug report data asynchronous.
    /// </summary>
    /// <param name="addBugReportModel">The add bug report model.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel tasks.</param>
    /// <returns>The boolean for success/failure.</returns>
    Task<bool> SubmitBugReportDataAsync(
        BugReportDTO addBugReportModel,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the lookup master data async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token used to cancel tasks.</param>
    /// <returns>The list of <see cref="LookupMasterDTO"/></returns>
    Task<IEnumerable<LookupMasterDTO>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    );
}
