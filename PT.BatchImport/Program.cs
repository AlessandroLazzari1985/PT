using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Extensions.Logging;
using PT.BatchImport.Importers;
using PT.BatchImport.NlogExtensions;
using PT.Data;

namespace PT.BatchImport
{
	class Program
	{
		static void Main(string[] args)
		{
			var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{environmentName}.json", optional: false, reloadOnChange: true);

			IConfigurationRoot configuration = builder.Build();

			var defaultConnection = configuration.GetConnectionString("DefaultConnection").Replace(@"\\", @"\");

			var serviceProvider = new ServiceCollection()
				.RegisterBatchImportDependencies()
				.AddDbContext<PTContext>(option => option.UseSqlServer(defaultConnection))
				.AddLogging(logging =>
				{
					logging.ClearProviders();
					logging.AddConsole();
					logging.AddNLog();
				})
				.BuildServiceProvider();

			var context = serviceProvider.GetService<PTContext>();

			// Creazione/Aggiornamento del database
			context.GetInfrastructure().GetService<IMigrator>().Migrate();

			var isModelCorrect = DatabaseHaveAllMigrations(context);

			LogManager.Configuration = isModelCorrect
				? NLogConfigurationManager.BuildDatabaseConfiguration(defaultConnection)
				: NLogConfigurationManager.BuildFileConfiguration();

			var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
			logger.LogInformation($"Environment: {environmentName}");

			var startTime = DateTime.Now;
			logger.LogInformation($"Start batch at: {startTime.ToLongTimeString()}");

			try
			{
				var iso3166Importer = serviceProvider.GetService<Iso3166Importer>();
				var countryImporter = serviceProvider.GetService<CountryImporter>();
				var countryStage = iso3166Importer.ImportCountiesStage(Iso3166.all1);
				countryImporter.SaveCountries(countryStage);
			}
			catch (Exception ex)
			{
				logger.LogError(ex, ex.Message);
				Task.Delay(2000);
			}



			var endTime = DateTime.Now;
			logger.LogInformation($"End batch at: {endTime.ToLongTimeString()}");
		}

		public static bool DatabaseHaveAllMigrations(PTContext context)
		{
			var migrazioniApplicate = context.Database.GetAppliedMigrations().ToList();
			var migrazioniPreviste = context.Database.GetMigrations().ToList();

			var tutteMigrazioniAppliate = migrazioniPreviste.All(x => migrazioniApplicate.Contains(x));

			return tutteMigrazioniAppliate;
		}
	}
}
