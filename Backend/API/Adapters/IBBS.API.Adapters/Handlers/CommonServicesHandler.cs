using IBBS.API.Adapters.Contracts;
using IBBS.API.Adapters.Models;
using IBBS.Domain.DrivingPorts;
using static IBBS.API.Adapters.Mapping.DomainToResponseMapper;
using static IBBS.API.Adapters.Mapping.RequestToDomainMapper;

namespace IBBS.API.Adapters.Handlers;

/// <summary>
/// The common services handler.
/// </summary>
/// <param name="commonService">The common services.</param>
/// <seealso cref="ICommonServicesHandler" />
public sealed class CommonServicesHandler(ICommonService commonService) : ICommonServicesHandler
{
    /// <inheritdoc />
    public async Task<IEnumerable<LookupMasterDTO>> GetLookupMasterDataAsync(
        CancellationToken cancellationToken = default
    )
    {
        var domainResult = await commonService.GetLookupMasterDataAsync(
            cancellationToken
        ).ConfigureAwait(false);
        return [.. domainResult.Select(MapToResponse)];
    }

    /// <inheritdoc />
    public async Task<bool> SubmitBugReportDataAsync(
        BugReportDTO addBugReportModel,
        CancellationToken cancellationToken = default
    )
    {
        var domainInput = MapToDomain(requestDto: addBugReportModel);
        return await commonService.SubmitBugReportDataAsync(
            addBugReportModel: domainInput,
            cancellationToken
        ).ConfigureAwait(false);
    }
}
