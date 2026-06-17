using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.DrivingPorts;
using IBBS.Domain.Helpers;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Domain.Helpers.DomainConstants;

namespace IBBS.Domain.UseCases;

/// <summary>
/// The common service domain class.
/// </summary>
/// <param name="commonDataManager">The common data manager.</param>
/// <param name="logger">The logger service.</param>
/// <param name="correlationContext">The correlation context used to track requests.</param>
/// <seealso cref="ICommonService" />
public sealed class CommonService(
    ICorrelationContext correlationContext,
    ILogger<CommonService> logger,
    ICommonDataManager commonDataManager) : ICommonService
{
    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterDomain>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<LookupMasterDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId })
            );

            response = await commonDataManager.GetLookupMasterDataAsync(
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> SubmitBugReportDataAsync(
        BugReportDomain addBugReportModel,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.MethodStartedMessageConstant,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, addBugReportModel })
            );

            addBugReportModel.ModifiedBy = addBugReportModel.CreatedBy;
            response = await commonDataManager.SubmitBugReportDataAsync(
                newBugReportData: addBugReportModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.MethodFailedWithMessageConstant, nameof(SubmitBugReportDataAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.MethodEndedMessageConstant,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, addBugReportModel, response })
            );
        }
    }
}
