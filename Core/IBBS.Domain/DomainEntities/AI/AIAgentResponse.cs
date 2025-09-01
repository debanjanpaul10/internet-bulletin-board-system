﻿namespace IBBS.Domain.DomainEntities.AI;

/// <summary>
/// The AI Agent Response.
/// </summary>
public class AIAgentResponse
{
	/// <summary>
	/// Gets or sets the status code.
	/// </summary>
	/// <value>
	/// The status code.
	/// </value>
	public int StatusCode { get; set; }

	/// <summary>
	/// Gets or sets a value indicating whether this instance is success.
	/// </summary>
	/// <value>
	///   <c>true</c> if this instance is success; otherwise, <c>false</c>.
	/// </value>
	public bool IsSuccess { get; set; }

	/// <summary>
	/// Gets or sets the response data.
	/// </summary>
	/// <value>
	/// The response data.
	/// </value>
	public object ResponseData { get; set; } = default!;
}
