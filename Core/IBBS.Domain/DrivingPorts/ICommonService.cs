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
	/// <returns>The boolean for success/failure.</returns>
	Task<bool> SubmitBugReportDataAsync(BugReportDomain addBugReportModel);
}
