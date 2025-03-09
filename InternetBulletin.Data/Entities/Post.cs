// *********************************************************************************
//	<copyright file="Post.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>The Post Entity Class.</summary>
// *********************************************************************************
namespace InternetBulletin.Data.Entities
{
	/// <summary>
	/// The Post Entity Class.
	/// </summary>
	public class Post
	{
		/// <summary>
		/// Gets or sets the post identifier.
		/// </summary>
		/// <value>
		/// The post identifier.
		/// </value>
		public Guid PostId { get; set; }

		/// <summary>
		/// Gets or sets the post title.
		/// </summary>
		/// <value>
		/// The post title.
		/// </value>
		public string PostTitle { get; set; }

		/// <summary>
		/// Gets or sets the content of the post.
		/// </summary>
		/// <value>
		/// The content of the post.
		/// </value>
		public string PostContent { get; set; }

		/// <summary>
		/// Gets or sets the post created date.
		/// </summary>
		/// <value>
		/// The post created date.
		/// </value>
		public DateTime PostCreatedDate { get; set; }

		/// <summary>
		/// Gets or sets the post created by.
		/// </summary>
		/// <value>
		/// The post created by.
		/// </value>
		public string PostCreatedBy { get; set; }

		/// <summary>
		/// Gets or sets the post modified date.
		/// </summary>
		/// <value>
		/// The post modified date.
		/// </value>
		public DateTime PostModifiedDate { get; set; }

		/// <summary>
		/// Gets or sets the post modified by.
		/// </summary>
		/// <value>
		/// The post modified by.
		/// </value>
		public string PostModifiedBy { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether this instance is active.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is active; otherwise, <c>false</c>.
		/// </value>
		public bool IsActive { get; set; }

	}
}
