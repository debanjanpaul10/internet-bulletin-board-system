using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBBS.Domain.DomainEntities.Posts;

/// <summary>
/// The user post rating domain model.
/// </summary>
public class UserPostRatingDomain
{
	/// <summary>
	/// Gets or sets the post id.
	/// </summary>
	/// <value>
	/// The post id.
	/// </value>
	public Guid PostId { get; set; }

	/// <summary>
	/// Gets or sets the is update success.
	/// </summary>
	/// <value>
	/// The is update success.
	/// </value>
	public bool IsUpdateSuccess { get; set; }

	/// <summary>
	/// Gets or sets the has already updated.
	/// </summary>
	/// <value>
	/// The has already updated.
	/// </value>
	public bool HasAlreadyUpdated { get; set; }
}
