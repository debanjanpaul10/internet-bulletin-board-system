namespace IBBS.Domain.DrivenPorts;

/// <summary>
/// The Mongo DB database manager.
/// </summary>
public interface IMongoDbDatabaseManager
{
	/// <summary>
	/// Gets the data from collection asynchronous.
	/// </summary>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="databaseName">Name of the database.</param>
	/// <param name="collectionName">Name of the collection.</param>
	/// <returns>The mongo db collection.</returns>
	Task<TResult> GetDataFromCollectionAsync<TResult>(string databaseName, string collectionName);
}
