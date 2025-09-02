using IBBS.Domain.DomainEntities.AI;

namespace IBBS.Domain.DrivenPorts;

public interface IMongoDbDatabaseManager
{
	/// <summary>
	/// Gets the application information data asynchronously.
	/// </summary>
	/// <returns>The about us details data <see cref="AboutUsAppInfoDataDomain"/></returns>
	Task<AboutUsAppInfoDataDomain> GetAboutUsDataAsync();
}
