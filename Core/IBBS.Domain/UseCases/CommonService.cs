using System.Globalization;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using Microsoft.Extensions.Logging;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The common service domain class.
/// </summary>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="IBBS.Domain.DrivingPorts.ICommonService" />
public class CommonService(ILogger<CommonService> logger, ICommonDataManager commonDataManager) : ICommonService
{
	/// <summary>
	/// Submits the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportModel">The add bug report model.</param>
	/// <returns>The boolean for success/failure.</returns>
	public async Task<bool> SubmitBugReportDataAsync(BugReportDomain addBugReportModel)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(SubmitBugReportDataAsync), DateTime.UtcNow, addBugReportModel.Title));
			return await commonDataManager.SubmitBugReportDataAsync(addBugReportModel).ConfigureAwait(false);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(SubmitBugReportDataAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(SubmitBugReportDataAsync), DateTime.UtcNow, addBugReportModel.Title));
		}
	}
}
