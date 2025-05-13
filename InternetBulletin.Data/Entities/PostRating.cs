// *********************************************************************************
//	<copyright file="PostRating.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Post Rating Entity Class.</summary>
// *********************************************************************************

namespace InternetBulletin.Data.Entities
{
    /// <summary>
    /// The Post Ratings Entity Class.
    /// </summary>
    public class PostRating
    {
        /// <summary>
        /// Gets or sets the post rating id.
        /// </summary>
        /// <value>
        /// The post rating id.
        /// </value>
        public int PostRatingId { get; set; }

        /// <summary>
        /// Gets or sets the post id.
        /// </summary>
        /// <value>
        /// The post id.
        /// </value>
        public Guid PostId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        /// <value>
        /// The user name.
        /// </value>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the rated on.
        /// </summary>
        /// <value>
        /// The rated on.
        /// </value>
        public DateTime RatedOn { get; set; }
    }
}