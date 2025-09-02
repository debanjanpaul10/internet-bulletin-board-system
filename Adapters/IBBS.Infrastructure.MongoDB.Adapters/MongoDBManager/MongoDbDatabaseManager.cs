using IBBS.Domain.DomainEntities.AI;
using IBBS.Domain.DrivenPorts;
using MongoDB.Bson;
using MongoDB.Driver;
using static IBBS.Infrastructure.MongoDB.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.MongoDB.Adapters.MongoDBManager;

/// <summary>
/// The MongoDB database manager class.
/// </summary>
/// <param name="mongoClient">The mongo database client.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IMongoDbDatabaseManager" />
public class MongoDbDatabaseManager(IMongoClient mongoClient) : IMongoDbDatabaseManager
{
	/// <summary>
	/// The ibbs database
	/// </summary>
	private readonly IMongoDatabase _ibbsDatabase = mongoClient.GetDatabase(MongoDBConstants.IbbsDatabase);

	/// <summary>
	/// The ai agents database
	/// </summary>
	private readonly IMongoDatabase _aiAgentsDatabase = mongoClient.GetDatabase(MongoDBConstants.AiAgentsKnowledgeBaseDatabase);

	/// <summary>
	/// Gets the application information data asynchronously.
	/// </summary>
	/// <returns>
	/// The about us details data <see cref="T:IBBS.Domain.DomainEntities.AI.AboutUsAppInfoDataDomain" />
	/// </returns>
	public async Task<AboutUsAppInfoDataDomain> GetAboutUsDataAsync()
	{
		var applicationInformation = _ibbsDatabase.GetCollection<ApplicationInformationDomain>(MongoDBConstants.ApplicationInformationCollectionConstant);
		var applicationTechnologies = _ibbsDatabase.GetCollection<ApplicationTechnologiesDomain>(MongoDBConstants.ApplicationTechnologiesCollectionConstant);
		if (applicationInformation is not null && applicationTechnologies is not null)
		{
			var applicationInfoResponseTask = applicationInformation.Find(_ => true).FirstAsync();
			var applicationTechnolgiesDataTask = applicationTechnologies.Find(new BsonDocument()).ToListAsync();
			await Task.WhenAll(applicationInfoResponseTask, applicationTechnolgiesDataTask).ConfigureAwait(false);

			var finalResult = new AboutUsAppInfoDataDomain
			{
				ApplicationTechnologiesData = applicationTechnolgiesDataTask.Result,
				ApplicationInformationData = applicationInfoResponseTask.Result
			};
			return finalResult;
		}

		throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
	}
}
