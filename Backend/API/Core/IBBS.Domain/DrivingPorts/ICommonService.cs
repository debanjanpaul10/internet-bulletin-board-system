using IBBS.Domain.DomainEntities;

namespace IBBS.Domain.DrivingPorts;

/// <summary>
/// The common service interface.
/// </summary>
public interface ICommonService
{
    /// <summary>
    /// Submits the bug report data asynchronous.
    /// </summary>
    /// <param name="addBugReportModel">The add bug report model.</param>
    /// <param name="cancellationToken">The cancellation token used to cancel requests.</param>
    /// <returns>The boolean for success/failure.</returns>
    Task<bool> SubmitBugReportDataAsync(
        BugReportDomain addBugReportModel,
        CancellationToken cancellationToken = default
    );

    /// <summary>
    /// Gets the lookup master data async.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token used to cancel requests.</param>
    /// <returns>The list of <see cref="LookupMasterDomain"/></returns>
    Task<IEnumerable<LookupMasterDomain>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    );
}
