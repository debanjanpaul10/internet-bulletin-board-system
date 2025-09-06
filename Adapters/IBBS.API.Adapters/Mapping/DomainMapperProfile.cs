using AutoMapper;
using IBBS.API.Adapters.Models;
using IBBS.API.Adapters.Models.AI;
using IBBS.API.Adapters.Models.Posts;
using IBBS.API.Adapters.Models.Users;
using IBBS.Domain.DomainEntities;
using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DomainEntities.Posts;

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
		CreateMap<UserQueryRequestDTO, UserQueryRequestDomain>();
		CreateMap<AIResponseFeedbackDTO, AIResponseFeedbackDomain>();

		CreateMap<PostRatingDomain, PostRatingDTO>().ReverseMap();
		CreateMap<PostWithRatingsDomain, PostWithRatingsDTO>().ReverseMap();
		CreateMap<UserPostRatingDomain, UserPostRatingDTO>().ReverseMap();
		CreateMap<UpdateRatingDomain, UpdateRatingDTO>();
		CreateMap<AIChatbotResponseDomain, AIChatbotResponseDTO>();
		CreateMap<LookupMasterDomain, LookupMasterDTO>();
		CreateMap<UserPostDomain, UserPostDTO>();
		CreateMap<UserProfileDomain, UserProfileDto>();
	}
}
