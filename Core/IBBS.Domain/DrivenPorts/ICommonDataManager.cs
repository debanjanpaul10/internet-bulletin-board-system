﻿using IBBS.Domain.DomainEntities;

namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The common data manager service.
/// </summary>
public interface ICommonDataManager
{
	/// <summary>
	/// Executes the aisql query asynchronous.
	/// </summary>
	/// <param name="aiSqlQuery">The ai SQL query.</param>
	/// <returns>The json format of the sql response.</returns>
	Task<string> ExecuteAISQLQueryAsync(string aiSqlQuery);

	/// <summary>
	/// Gets the sample prompts for chatbot asynchronous.
	/// </summary>
	/// <returns>The list of <see cref="LookupMasterDomain"/></returns>
	Task<IEnumerable<LookupMasterDomain>> GetSamplePromptsForChatbotAsync();
}
