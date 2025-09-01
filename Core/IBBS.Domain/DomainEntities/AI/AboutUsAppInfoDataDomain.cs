using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The About us and application information data dto.
/// </summary>
public class AboutUsAppInfoDataDomain
{
	/// <summary>
	/// The application information data.
	/// </summary>
	public ApplicationInformationDomain ApplicationInformationData { get; set; } = new();

	/// <summary>
	/// The application technologies data.
	/// </summary>
	public List<ApplicationTechnologiesDomain> ApplicationTechnologiesData { get; set; } = [];
}

/// <summary>
/// The Application Information Data DTO.
/// </summary>
public class ApplicationInformationDomain
{
	/// <summary>
	/// The Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// The Title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The IntroductionDomain
	/// </summary>
	[BsonElement("introduction")]
	public IntroductionDomain Introduction { get; set; } = new();

	/// <summary>
	/// The data object for what is IBBS.
	/// </summary>
	[BsonElement("whatIsIBBS")]
	public WhatIsIBBSDomain WhatIsIBBS { get; set; } = new();

	/// <summary>
	/// The key features object.
	/// </summary>
	[BsonElement("keyFeatures")]
	public KeyFeaturesDomain KeyFeatures { get; set; } = new();

	/// <summary>
	/// How to use object.
	/// </summary>
	[BsonElement("howToUse")]
	public HowToUseDomain HowToUse { get; set; } = new();

	/// <summary>
	/// The technical excellence object.
	/// </summary>
	[BsonElement("technicalExcellence")]
	public TechnicalExcellenceDomain TechnicalExcellence { get; set; } = new();

	/// <summary>
	/// The upcoming features object.
	/// </summary>
	[BsonElement("upcomingFeatures")]
	public UpcomingFeaturesDomain UpcomingFeatures { get; set; } = new();
}

/// <summary>
/// The application technologies data dto.
/// </summary>
[BsonIgnoreExtraElements]
public class ApplicationTechnologiesDomain
{
	/// <summary>
	/// The Id.
	/// </summary>
	[BsonId]
	[BsonRepresentation(BsonType.ObjectId)]
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// The heading.
	/// </summary>
	[BsonElement("Heading")]
	public string Heading { get; set; } = string.Empty;

	/// <summary>
	/// The description.
	/// </summary>
	[BsonElement("Description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// The link.
	/// </summary>
	[BsonElement("Link")]
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// The image.
	/// </summary>
	[BsonElement("Image")]
	public string Image { get; set; } = string.Empty;
}

/// <summary>
/// The feature section.
/// </summary>
public class FeatureSectionDomain
{
	/// <summary>
	/// The feature section title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The features section items.
	/// </summary>
	[BsonElement("items")]
	public List<string> Items { get; set; } = [];
}

/// <summary>
/// How to use data.
/// </summary>
public class HowToUseDomain
{
	/// <summary>
	/// The how to use title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The how to use sections.
	/// </summary>
	[BsonElement("sections")]
	public List<UsageSectionDomain> Sections { get; set; } = [];
}

/// <summary>
/// The introduction.
/// </summary>
public class IntroductionDomain
{
	/// <summary>
	/// The title of introduction.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The content of introduction.
	/// </summary>
	[BsonElement("content")]
	public string Content { get; set; } = string.Empty;
}

/// <summary>
/// The Key features.
/// </summary>
public class KeyFeaturesDomain
{
	/// <summary>
	/// The key features title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The key features section.
	/// </summary>
	[BsonElement("sections")]
	public List<FeatureSectionDomain> Sections { get; set; } = [];
}

/// <summary>
/// The Technical Excellence.
/// </summary>
public class TechnicalExcellenceDomain
{
	/// <summary>
	/// The technical excellence title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The technical excellence sections.
	/// </summary>
	[BsonElement("sections")]
	public List<FeatureSectionDomain> Sections { get; set; } = [];
}

/// <summary>
/// The upcoming features.
/// </summary>
public class UpcomingFeaturesDomain
{
	/// <summary>
	/// The upcoming features title
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The upocoming features sections.
	/// </summary>
	[BsonElement("sections")]
	public List<FeatureSectionDomain> Sections { get; set; } = [];
}

/// <summary>
/// The usage section.
/// </summary>
public class UsageSectionDomain
{
	/// <summary>
	/// The usage section title.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The usage content.
	/// </summary>
	[BsonElement("content")]
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// The usage section items.
	/// </summary>
	[BsonElement("items")]
	public List<string> Items { get; set; } = [];
}

/// <summary>
/// What is IBBS data.
/// </summary>
public class WhatIsIBBSDomain
{
	/// <summary>
	/// The title for what is ibbs.
	/// </summary>
	[BsonElement("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The content for what is ibbs.
	/// </summary>
	[BsonElement("content")]
	public string Content { get; set; } = string.Empty;
}