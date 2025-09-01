namespace IBBS.API.Adapters.Models;

/// <summary>
/// The About us and application information data dto.
/// </summary>
public class AboutUsAppInfoDataDTO
{
	/// <summary>
	/// The application information data.
	/// </summary>
	public ApplicationInformation ApplicationInformationData { get; set; } = new();

	/// <summary>
	/// The application technologies data.
	/// </summary>
	public List<ApplicationTechnologies> ApplicationTechnologiesData { get; set; } = [];
}

/// <summary>
/// The Application Information Data DTO.
/// </summary>
public class ApplicationInformation
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
	public Introduction Introduction { get; set; } = new();

	/// <summary>
	/// The data object for what is IBBS.
	/// </summary>
	public WhatIsIBBS WhatIsIBBS { get; set; } = new();

	/// <summary>
	/// The key features object.
	/// </summary>
	public KeyFeatures KeyFeatures { get; set; } = new();

	/// <summary>
	/// How to use object.
	/// </summary>
	public HowToUse HowToUse { get; set; } = new();

	/// <summary>
	/// The technical excellence object.
	/// </summary>
	public TechnicalExcellence TechnicalExcellence { get; set; } = new();

	/// <summary>
	/// The upcoming features object.
	/// </summary>
	public UpcomingFeatures UpcomingFeatures { get; set; } = new();
}

/// <summary>
/// The application technologies data dto.
/// </summary>
public class ApplicationTechnologies
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
public class FeatureSection
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
public class HowToUse
{
	/// <summary>
	/// The how to use title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The how to use sections.
	/// </summary>
	public List<UsageSection> Sections { get; set; } = [];
}

/// <summary>
/// The introduction.
/// </summary>
public class Introduction
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
public class KeyFeatures
{
	/// <summary>
	/// The key features title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The key features section.
	/// </summary>
	public List<FeatureSection> Sections { get; set; } = [];
}

/// <summary>
/// The Technical Excellence.
/// </summary>
public class TechnicalExcellence
{
	/// <summary>
	/// The technical excellence title.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The technical excellence sections.
	/// </summary>
	public List<FeatureSection> Sections { get; set; } = [];
}

/// <summary>
/// The upcoming features.
/// </summary>
public class UpcomingFeatures
{
	/// <summary>
	/// The upcoming features title
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The upocoming features sections.
	/// </summary>
	public List<FeatureSection> Sections { get; set; } = [];
}

/// <summary>
/// The usage section.
/// </summary>
public class UsageSection
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
public class WhatIsIBBS
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