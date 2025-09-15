﻿using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.API.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using static IBBS.API.Helpers.APIConstants;
using static IBBS.API.Helpers.SwaggerConstants.CommonServicesController;

namespace IBBS.API.Controllers;

/// <summary>
/// The common services controller class.
/// </summary>
/// <param name="commonServicesHandler">The common services handler.</param>
/// <param name="httpContextAccessor">The http context accessor.</param>
/// <seealso cref="IBBS.API.Controllers.BaseController" />
[ApiController]
[Route(RouteConstants.CommonServicesController.BaseRoute)]
public class CommonServicesController(IHttpContextAccessor httpContextAccessor, ICommonServicesHandler commonServicesHandler) : BaseController(httpContextAccessor)
{
	/// <summary>
	/// Submits the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportModel">The add bug report model.</param>
	/// <returns>The boolean for success/failure.</returns>
	/// <exception cref="System.ArgumentNullException"></exception>
	[HttpPost(RouteConstants.CommonServicesController.SubmitBugReport_Route)]
	[ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
	[ProducesResponseType(StatusCodes.Status401Unauthorized)]
	[ProducesResponseType(StatusCodes.Status400BadRequest)]
	[ProducesResponseType(StatusCodes.Status404NotFound)]
	[SwaggerOperation(Summary = SubmitBugReportDataAction.Summary, Description = SubmitBugReportDataAction.Description, OperationId = SubmitBugReportDataAction.OperationId)]
	public async Task<IActionResult> SubmitBugReportDataAsync([FromBody] BugReportDTO addBugReportModel)
	{
		if (IsAuthorized())
		{
			ArgumentNullException.ThrowIfNull(addBugReportModel);
			addBugReportModel.CreatedBy = UserEmail;
			var result = await commonServicesHandler.SubmitBugReportDataAsync(addBugReportModel).ConfigureAwait(false);
			if (result)
			{
				return HandleSuccessResult(result);
			}

			return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
		}

		return HandleUnAuthorizedRequest();
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
	[SwaggerOperation(Summary = GetLookupMasterDataAction.Summary, Description = GetLookupMasterDataAction.Description, OperationId = GetLookupMasterDataAction.OperationId)]
	public async Task<IActionResult> GetLookupMasterDataAsync()
	{
		var result = await commonServicesHandler.GetLookupMasterDataAsync().ConfigureAwait(false);
		if (result is not null && result.Any())
		{
			return HandleSuccessResult(result);
		}

		return HandleBadRequest(ExceptionConstants.SomethingWentWrongMessageConstant);
	}
}
