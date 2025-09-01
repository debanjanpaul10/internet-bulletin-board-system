using AutoMapper;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using InternetBulletin.Shared.DTOs.AI;

namespace IBBS.API.Adapters.Mapping;

/// <summary>
/// The Domain Mapper Profile.
/// </summary>
/// <seealso cref="AutoMapper.Profile" />
public class DomainMapperProfile : Profile
{
	/// <summary>
	/// Initializes a new instance of the <see cref="DomainMapperProfile"/> class.
	/// </summary>
	public DomainMapperProfile()
	{
		CreateMap<UserStoryRequestDTO, UserStoryRequestDomain>();
		CreateMap<AboutUsAppInfoDataDomain, AboutUsAppInfoDataDTO>();
		CreateMap<PostRatingDomain, PostRatingDTO>().ReverseMap();
		CreateMap<UpdateRatingDomain, UpdateRatingDTO>();
	}
}
