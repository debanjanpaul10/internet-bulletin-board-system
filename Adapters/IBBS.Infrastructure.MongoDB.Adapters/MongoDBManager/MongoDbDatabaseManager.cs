using System.Globalization;
using IBBS.Domain.DrivenPorts;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using static IBBS.Infrastructure.MongoDB.Adapters.Helpers.Constants;

namespace IBBS.Infrastructure.MongoDB.Adapters.MongoDBManager;

/// <summary>
/// The MongoDB database manager class.
/// </summary>
/// <param name="mongoClient">The mongo database client.</param>
/// <seealso cref="IBBS.Domain.DrivenPorts.IMongoDbDatabaseManager" />
public class MongoDbDatabaseManager(IMongoClient mongoClient, ILogger<MongoDbDatabaseManager> logger) : IMongoDbDatabaseManager
{
	/// <summary>
	/// Gets the data from collection asynchronous.
	/// </summary>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="databaseName">Name of the database.</param>
	/// <param name="collectionName">Name of the collection.</param>
	/// <returns>
	/// The mongo db collection.
	/// </returns>
	/// <exception cref="System.Exception"></exception>
	public async Task<TResult> GetDataFromCollectionAsync<TResult>(string databaseName, string collectionName)
	{
		try
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodStartedMessageConstant, nameof(GetDataFromCollectionAsync), DateTime.UtcNow));
			var mongoDatabase = mongoClient.GetDatabase(databaseName);
			var collectionData = mongoDatabase.GetCollection<TResult>(collectionName);
			if (collectionData is not null)
			{
				return await collectionData.Find(_ => true).FirstAsync().ConfigureAwait(false);
			}

			throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
		}
		catch (Exception ex)
		{
			logger.LogError(ex, string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodFailedWithMessageConstant, nameof(GetDataFromCollectionAsync), DateTime.UtcNow, ex.Message));
			throw;
		}
		finally
		{
			logger.LogInformation(string.Format(CultureInfo.CurrentCulture, LoggingConstants.MethodEndedMessageConstant, nameof(GetDataFromCollectionAsync), DateTime.UtcNow));
		}
	}
}
