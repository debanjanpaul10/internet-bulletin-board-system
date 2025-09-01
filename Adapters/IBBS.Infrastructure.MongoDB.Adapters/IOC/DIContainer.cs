using System.Xml.Xsl;

namespace IBBS.Infrastructure.MongoDB.Adapters.IOC;

public static class DIContainer
{

	/// <summary>
	/// Configures the MongoDB service.
	/// </summary>
	/// <param name="builder">The web application builder.</param>
	public static void ConfigureMongoDbServer(this WebApplicationBuilder builder)
	{
		var mongoConnectionString = builder.Configuration[MongoDbConnectionStringConstant];
		if (!string.IsNullOrEmpty(mongoConnectionString))
		{
			var settings = MongoClientSettings.FromConnectionString(mongoConnectionString);
			settings.SslSettings = new SslSettings() { EnabledSslProtocols = System.Security.Authentication.SslProtocols.Tls12 };
			builder.Services.AddSingleton<IMongoClient>(new MongoClient(settings));
		}
	}
}
