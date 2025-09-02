using AutoMapper;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.Posts;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;
using InternetBulletin.Shared.DTOs.AI;
using InternetBulletin.Shared.DTOs.Users;

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
		CreateMap<PostRatingDomain, PostRatingDTO>().ReverseMap();
		CreateMap<UserPostRatingDomain, UserPostRatingDTO>().ReverseMap();
		CreateMap<UserStoryRequestDTO, UserStoryRequestDomain>();
		CreateMap<UpdateRatingDomain, UpdateRatingDTO>();

		CreateMap<AboutUsAppInfoDataDomain, AboutUsAppInfoDataDTO>();
		CreateMap<ApplicationInformationDomain, ApplicationInformationDTO>();
		CreateMap<ApplicationTechnologiesDomain, ApplicationTechnologiesDTO>();
		CreateMap<IntroductionDomain, IntroductionDTO>();
		CreateMap<WhatIsIBBSDomain, WhatIsIBBSDTO>();
		CreateMap<KeyFeaturesDomain, KeyFeaturesDTO>();
		CreateMap<HowToUseDomain, HowToUseDTO>();
		CreateMap<TechnicalExcellenceDomain, TechnicalExcellenceDTO>();
		CreateMap<UpcomingFeaturesDomain, UpcomingFeaturesDTO>();
		CreateMap<UsageSectionDomain, UsageSectionDTO>();
		CreateMap<FeatureSectionDomain, FeatureSectionDTO>();
	}
}
