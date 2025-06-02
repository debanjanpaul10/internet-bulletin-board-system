// *********************************************************************************
//	<copyright file="ApplicationInformationData.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Application Information Data DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.ApplicationInfo
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;

    /// <summary>
    /// The Application Information Data DTO.
    /// </summary>
    public class ApplicationInformationDataDTO
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
        /// The Introduction
        /// </summary>
        [BsonElement("introduction")]
        public Introduction Introduction { get; set; } = new();

        /// <summary>
        /// The data object for what is IBBS.
        /// </summary>
        [BsonElement("whatIsIBBS")]
        public WhatIsIBBS WhatIsIBBS { get; set; } = new();

        /// <summary>
        /// The key features object.
        /// </summary>
        [BsonElement("keyFeatures")]
        public KeyFeatures KeyFeatures { get; set; } = new();

        /// <summary>
        /// How to use object.
        /// </summary>
        [BsonElement("howToUse")]
        public HowToUse HowToUse { get; set; } = new();

        /// <summary>
        /// The technical excellence object.
        /// </summary>
        [BsonElement("technicalExcellence")]
        public TechnicalExcellence TechnicalExcellence { get; set; } = new();

        /// <summary>
        /// The upcoming features object.
        /// </summary>
        [BsonElement("upcomingFeatures")]
        public UpcomingFeatures UpcomingFeatures { get; set; } = new();
    }

    /// <summary>
    /// The introduction.
    /// </summary>
    public class Introduction
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
    /// What is IBBS data.
    /// </summary>
    public class WhatIsIBBS
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

    /// <summary>
    /// The Key features.
    /// </summary>
    public class KeyFeatures
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
        public List<FeatureSection> Sections { get; set; } = [];
    }

    /// <summary>
    /// How to use data.
    /// </summary>
    public class HowToUse
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
        public List<UsageSection> Sections { get; set; } = [];
    }

    /// <summary>
    /// The Technical Excellence.
    /// </summary>
    public class TechnicalExcellence
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
        [BsonElement("title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The upocoming features sections.
        /// </summary>
        [BsonElement("sections")]
        public List<FeatureSection> Sections { get; set; } = [];
    }

    /// <summary>
    /// The feature section.
    /// </summary>
    public class FeatureSection
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
    /// The usage section.
    /// </summary>
    public class UsageSection
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
}


