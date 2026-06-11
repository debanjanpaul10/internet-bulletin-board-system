using IBBS.API.Adapters.Models;
using IBBS.Domain.DomainEntities;

namespace IBBS.API.Adapters.Mapping;

/// <summary>
/// The domain to response mapper for AI services.
/// </summary>
public static class DomainToResponseMapper
{
    /// <summary>
    /// Maps the lookup master domain model to response DTO.
    /// </summary>
    /// <param name="domain">The lookup master domain model.</param>
    /// <returns>The lookup master DTO.</returns>
    internal static LookupMasterDTO MapToResponse(
        LookupMasterDomain domain
    ) => new()
    {
        Id = domain.Id,
        KeyName = domain.KeyName,
        KeyValue = domain.KeyValue,
        Type = domain.Type
    };
}