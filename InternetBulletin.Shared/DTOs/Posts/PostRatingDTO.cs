// *********************************************************************************
//	<copyright file="PostRatingDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Post Ratings DTO.</summary>
// *********************************************************************************

namespace InternetBulletin.Shared.DTOs.Posts
{
    /// <summary>
    /// The Post Ratings DTO.
    /// </summary>
    public class PostRatingDTO
    {
        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        /// <value>
        /// The post id.
        /// </value>
        public string PostId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the is increment.
        /// </summary>
        /// <value>
        /// The incremented or decremented value.
        /// </value>
        public bool IsIncrement { get; set; }
    }
}