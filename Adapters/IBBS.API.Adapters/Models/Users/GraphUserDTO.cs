// *********************************************************************************
//	<copyright file="GraphUserDTO.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary>Graph user d t o.</summary>
// *********************************************************************************
namespace InternetBulletin.Shared.DTOs.Users
{
    /// <summary>
    /// Graph user d t o.
    /// </summary>
    public class GraphUserDTO
    {
        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        /// <value>
        /// The id.
        /// </value>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the display name.
        /// </summary>
        /// <value>
        /// The display name.
        /// </value>
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the user name extension.
        /// </summary>
        /// <value>
        /// The user name extension.
        /// </value>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the identities.
        /// </summary>
        /// <value>
        /// The identities.
        /// </value>
        public string EmailAddress { get; set; } = string.Empty;
    }
}


