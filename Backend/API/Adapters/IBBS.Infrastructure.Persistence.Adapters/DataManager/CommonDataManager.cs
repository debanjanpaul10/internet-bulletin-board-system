using IBBS.Domain.Contracts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DrivenPorts;
using IBBS.Domain.Helpers;
using IBBS.Infrastructure.Persistence.Adapters.Contracts;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static IBBS.Infrastructure.Persistence.Adapters.Helpers.Constants;
using static IBBS.Infrastructure.Persistence.Adapters.Mapper.DataMapperProfile;

namespace IBBS.Infrastructure.Persistence.Adapters.DataManager;

/// <summary>
/// The common data manager service.
/// </summary>
/// <param name="correlationContext">The correlation context.</param>
/// <param name="logger">The logger servoce.</param>
/// <param name="masterDataRepository">The master data repository.</param>
/// <seealso cref="Domain.DrivenPorts.ICommonDataManager" />
public sealed class CommonDataManager(
    ICorrelationContext correlationContext,
    ILogger<CommonDataManager> logger,
    IMasterDataRepository masterDataRepository) : ICommonDataManager
{
    /// <inheritdoc />
    public async Task<string> ExecuteAISQLQueryAsync(
        string aiSqlQuery,
        CancellationToken cancellationToken = default
    )
    {
        string response = string.Empty;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, aiSqlQuery })
            );

            response = await masterDataRepository.ExecuteAISQLQueryAsync(
                aiSqlQuery,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(ExecuteAISQLQueryAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, aiSqlQuery, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterDomain>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<LookupMasterDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, correlationContext.CorrelationId
            );

            var dbResponse = await masterDataRepository.GetLookupMasterDataAsync(cancellationToken).ConfigureAwait(false);
            response = [.. dbResponse.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
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
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync(
        CancellationToken cancellationToken = default
    )
    {
        IEnumerable<LookupMasterDomain> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, correlationContext.CorrelationId
            );

            var dbResponse = await masterDataRepository.GetSamplePromptsForChatbotAsync(cancellationToken).ConfigureAwait(false);
            response = [.. dbResponse.Select(MapToDomain)];
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetSamplePromptsForChatbotAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

    /// <inheritdoc />
    public async Task<bool> SubmitBugReportDataAsync(
        BugReportDomain newBugReportData,
        CancellationToken cancellationToken = default
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, correlationContext.CorrelationId
            );

            var entityModel = MapToEntity(domain: newBugReportData);
            response = await masterDataRepository.SubmitBugReportDataAsync(
                newBugReportData: entityModel,
                cancellationToken
            ).ConfigureAwait(false);
            return response;
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, ex.Message
            );
            throw new IBBSBusinessException(
                message: ex.Message,
                correlationId: correlationContext.CorrelationId
            );
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }

}
