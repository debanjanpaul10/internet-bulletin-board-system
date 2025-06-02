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
        /// Initializes a new instance of <see cref="BulletinService"/> class.
        /// </summary>
        /// <param name="mongoClient">The Mongo DB Client.</param>
        /// <param name="configuration">The configuration.</param>
        public BulletinService(IMongoClient mongoClient, IConfiguration configuration)
        {
            this._configuration = configuration;

            var databaseName = this._configuration[ConfigurationConstants.MongoDatabaseNameConstant];
            this._mongoDatabase = mongoClient.GetDatabase(databaseName);
        }

        /// <summary>
        /// Gets the application information data asynchronously.
        /// </summary>
        /// <returns>The application information data.</returns>
        /// <exception cref="Exception">The default exception thrown.</exception>
        public async Task<ApplicationInformationDataDTO> GetApplicationInformationDataAsync()
        {
            var collectionName = IbbsConstants.ApplicationInformationCollectionConstant;
            var collection = this._mongoDatabase.GetCollection<ApplicationInformationDataDTO>(collectionName);
            if (collection is not null)
            {
                var response = await collection.Find(_ => true).FirstAsync();
                return response;
            }

            throw new Exception(ExceptionConstants.SomethingWentWrongMessageConstant);
        }
    }
}


