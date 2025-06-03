// *********************************************************************************
//	<copyright file="BulletinService.cs" company="Personal">
//		Copyright (c) 2025 Personal
//	</copyright>
// <summary> The Bulletin Service class.</summary>
// *********************************************************************************
namespace InternetBulletin.Business.Services
{
	using InternetBulletin.Business.Contracts;
	using InternetBulletin.Shared.Constants;
	using InternetBulletin.Shared.DTOs.ApplicationInfo;
	using Microsoft.Extensions.Configuration;
	using MongoDB.Driver;

	/// <summary>
	/// The Bulletin Service class.
	/// </summary>
	/// <see cref="IBulletinService"/>
	public class BulletinService : IBulletinService
	{
		/// <summary>
		/// The mongo database.
		/// </summary>
		private readonly IMongoDatabase _mongoDatabase;

		/// <summary>
		/// The configuration.
		/// </summary>
		private readonly IConfiguration _configuration;

		/// <summary>
		/// The cache service.
		/// </summary>
		private readonly ICacheService _cacheService;

		/// <summary>
		/// Initializes a new instance of <see cref="BulletinService"/> class.
		/// </summary>
		/// <param name="mongoClient">The Mongo DB Client.</param>
		/// <param name="configuration">The configuration.</param>
		/// <param name="cacheService">The cache service.</param>
		public BulletinService(IMongoClient mongoClient, IConfiguration configuration, ICacheService cacheService)
		{
			this._configuration = configuration;

			var databaseName = this._configuration[ConfigurationConstants.MongoDatabaseNameConstant];
			this._mongoDatabase = mongoClient.GetDatabase(databaseName);
			this._cacheService = cacheService;
		}

		/// <summary>
		/// Gets the application information data asynchronously.
		/// </summary>
		/// <returns>The application information data.</returns>
		/// <exception cref="Exception">The default exception thrown.</exception>
		public async Task<ApplicationInformationDataDTO> GetApplicationInformationDataAsync()
		{
			var cacheData = this._cacheService.GetCachedData<ApplicationInformationDataDTO>(CacheKeys.ApplicationInformationDataCacheKey);
			if (cacheData is not null)
			{
				return cacheData;
			}
			else
			{
				var collectionName = IbbsConstants.ApplicationInformationCollectionConstant;
				var collection = this._mongoDatabase.GetCollection<ApplicationInformationDataDTO>(collectionName);
				if (collection is not null)
				{
					var response = await collection.Find(_ => true).FirstAsync();
					this._cacheService.SetCacheData(CacheKeys.ApplicationInformationDataCacheKey, response, TimeSpan.FromMinutes(30));
					return response;
				}

				throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
			}
		}
	}
}


