using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Helpers;
using IBBS.Domain.Contracts;
using IBBS.Domain.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.CommonServicesController;

namespace IBBS.API.Controllers.v1;

/// <summary>
/// The common services controller class.
/// </summary>
/// <param name="commonServicesHandler">The common services handler.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <param name="configuration">The configuration service.</param>
/// <param name="correlationContext">The correlation context used for logging requests.</param>
/// <param name="logger">The logger service.</param>
/// <seealso cref="BaseController" />
[ApiController]
[Route(RouteConstants.CommonServicesController.BaseRoute)]
public sealed class CommonServicesController(
    IHttpContextAccessor httpContextAccessor,
    IConfiguration configuration,
    ICorrelationContext correlationContext,
    ILogger<CommonServicesController> logger,
    ICommonServicesHandler commonServicesHandler) : BaseController(httpContextAccessor, configuration)
{
    /// <summary>
    /// Submits the bug report data asynchronous.
    /// </summary>
    /// <param name="addBugReportModel">The add bug report model.</param>
    /// <returns>The boolean for success/failure.</returns>
    [HttpPost(RouteConstants.CommonServicesController.SubmitBugReport_Route)]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = SubmitBugReportDataAction.Summary,
        Description = SubmitBugReportDataAction.Description,
        OperationId = SubmitBugReportDataAction.OperationId)]
    public async Task<IActionResult> SubmitBugReportDataAsync(
        [FromBody] BugReportDTO addBugReportModel
    )
    {
        bool response = false;
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, addBugReportModel, UserEmail })
            );

            ArgumentNullException.ThrowIfNull(addBugReportModel);
            if (base.IsAuthorized(authorizationTypes: AuthorizationTypes.UserBased))
            {
                addBugReportModel.CreatedBy = UserEmail;
                response = await commonServicesHandler.SubmitBugReportDataAsync(
                    addBugReportModel,
                    cancellationToken: HttpContext.RequestAborted
                ).ConfigureAwait(false);

                return base.HandleSuccessResult(response);
            }

            return base.HandleUnAuthorizedRequest();
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(SubmitBugReportDataAsync), DateTime.UtcNow,
                    JsonConvert.SerializeObject(new { correlationContext.CorrelationId, addBugReportModel, UserEmail, response })
            );
        }
    }

    /// <summary>
    /// Gets the lookup master data asynchronous.
    /// </summary>
    /// <returns>The list of <see cref="LookupMasterDTO"/></returns>
    [AllowAnonymous]
    [HttpGet(RouteConstants.CommonServicesController.GetLookupMasterData_Route)]
    [ProducesResponseType(typeof(IEnumerable<LookupMasterDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [SwaggerOperation(
        Summary = GetLookupMasterDataAction.Summary,
        Description = GetLookupMasterDataAction.Description,
        OperationId = GetLookupMasterDataAction.OperationId)]
    public async Task<IActionResult> GetLookupMasterDataAsync()
    {
        IEnumerable<LookupMasterDTO> response = [];
        try
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodStart,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId })
            );

            response = await commonServicesHandler.GetLookupMasterDataAsync(
                cancellationToken: HttpContext.RequestAborted
            ).ConfigureAwait(false);
            return base.HandleSuccessResult(response);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleTaskCancelledResponse(message: ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogAppError(
                ex,
                LoggingConstants.LogHelperMethodFailed,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, ex.Message })
            );
            return base.HandleBadRequest(message: ex.Message);
        }
        finally
        {
            logger.LogAppInformation(
                LoggingConstants.LogHelperMethodEnded,
                nameof(GetLookupMasterDataAsync), DateTime.UtcNow, JsonConvert.SerializeObject(new { correlationContext.CorrelationId, response })
            );
        }
    }
}
