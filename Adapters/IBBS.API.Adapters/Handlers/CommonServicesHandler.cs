using AutoMapper;
using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DrivingPorts;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The common services handler.
/// </summary>
/// <param name="commonService">The common services.</param>
/// <param name="mapper">The auto mapper.</param>
/// <seealso cref="IBBS.API.Adapters.Contracts.ICommonServicesHandler" />
public class CommonServicesHandler(IMapper mapper, ICommonService commonService) : ICommonServicesHandler
{
	/// <summary>
	/// Gets the lookup master data async.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDTO"/></returns>
	public async Task<IEnumerable<LookupMasterDTO>> GetLookupMasterDataAsync()
	{
		var domainResult = await commonService.GetLookupMasterDataAsync().ConfigureAwait(false);
		return mapper.Map<IEnumerable<LookupMasterDTO>>(domainResult);
	}

	/// <summary>
	/// Submits the bug report data asynchronous.
	/// </summary>
	/// <param name="addBugReportModel">The add bug report model.</param>
	/// <returns>
	/// The boolean for success/failure.
	/// </returns>
	public async Task<bool> SubmitBugReportDataAsync(BugReportDTO addBugReportModel)
	{
		var domainInput = mapper.Map<BugReportDomain>(addBugReportModel);
		return await commonService.SubmitBugReportDataAsync(domainInput).ConfigureAwait(false);
	}
}
