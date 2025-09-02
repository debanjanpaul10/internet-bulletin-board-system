namespace IBBS.API.Adapters.Models;

/// <summary>
/// The About us and application information data dto.
/// </summary>
public class AboutUsAppInfoDataDTO
{
	/// <summary>
	/// The application information data.
	/// </summary>
	public ApplicationInformationDTO ApplicationInformationData { get; set; } = new();

	/// <summary>
	/// The application technologies data.
	/// </summary>
	public List<ApplicationTechnologiesDTO> ApplicationTechnologiesData { get; set; } = [];
}

/// <summary>
/// The Application Information Data DTO.
/// </summary>
public class ApplicationInformationDTO
{
	/// <summary>
	/// The Id.
	/// </summary>
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// The Title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The IntroductionDomain
	/// </summary>
	public IntroductionDTO Introduction { get; set; } = new();

	/// <summary>
	/// The data object for what is IBBS.
	/// </summary>
	public WhatIsIBBSDTO WhatIsIBBS { get; set; } = new();

	/// <summary>
	/// The key features object.
	/// </summary>
	public KeyFeaturesDTO KeyFeatures { get; set; } = new();

	/// <summary>
	/// How to use object.
	/// </summary>
	public HowToUseDTO HowToUse { get; set; } = new();

	/// <summary>
	/// The technical excellence object.
	/// </summary>
	public TechnicalExcellenceDTO TechnicalExcellence { get; set; } = new();

	/// <summary>
	/// The upcoming features object.
	/// </summary>
	public UpcomingFeaturesDTO UpcomingFeatures { get; set; } = new();
}

/// <summary>
/// The application technologies data dto.
/// </summary>
public class ApplicationTechnologiesDTO
{
	/// <summary>
	/// The Id.
	/// </summary>
	public string Id { get; set; } = string.Empty;

	/// <summary>
	/// The heading.
	/// </summary>
	public string Heading { get; set; } = string.Empty;

	/// <summary>
	/// The description.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// The link.
	/// </summary>
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// The image.
	/// </summary>
	public string Image { get; set; } = string.Empty;
}

/// <summary>
/// The feature section.
/// </summary>
public class FeatureSectionDTO
{
	/// <summary>
	/// The feature section title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The features section items.
	/// </summary>
	public List<string> Items { get; set; } = [];
}

/// <summary>
/// How to use data.
/// </summary>
public class HowToUseDTO
{
	/// <summary>
	/// The how to use title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The how to use sections.
	/// </summary>
	public List<UsageSectionDTO> Sections { get; set; } = [];
}

/// <summary>
/// The introduction.
/// </summary>
public class IntroductionDTO
{
	/// <summary>
	/// The title of introduction.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The content of introduction.
	/// </summary>
	public string Content { get; set; } = string.Empty;
}

/// <summary>
/// The Key features.
/// </summary>
public class KeyFeaturesDTO
{
	/// <summary>
	/// The key features title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The key features section.
	/// </summary>
	public List<FeatureSectionDTO> Sections { get; set; } = [];
}

/// <summary>
/// The Technical Excellence.
/// </summary>
public class TechnicalExcellenceDTO
{
	/// <summary>
	/// The technical excellence title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The technical excellence sections.
	/// </summary>
	public List<FeatureSectionDTO> Sections { get; set; } = [];
}

/// <summary>
/// The upcoming features.
/// </summary>
public class UpcomingFeaturesDTO
{
	/// <summary>
	/// The upcoming features title
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The upocoming features sections.
	/// </summary>
	public List<FeatureSectionDTO> Sections { get; set; } = [];
}

/// <summary>
/// The usage section.
/// </summary>
public class UsageSectionDTO
{
	/// <summary>
	/// The usage section title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The usage content.
	/// </summary>
	public string Content { get; set; } = string.Empty;

	/// <summary>
	/// The usage section items.
	/// </summary>
	public List<string> Items { get; set; } = [];
}

/// <summary>
/// What is IBBS data.
/// </summary>
public class WhatIsIBBSDTO
{
	/// <summary>
	/// The title for what is ibbs.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The content for what is ibbs.
	/// </summary>
	public string Content { get; set; } = string.Empty;
}