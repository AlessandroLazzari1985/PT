using System.ComponentModel;
using Microsoft.Extensions.DependencyInjection;
using PT.BatchImport.Importers;

namespace PT.BatchImport
{
	public static class BatchImportContainer
	{
		public static IServiceCollection RegisterBatchImportDependencies(this IServiceCollection collection)
		{
			collection.AddSingleton<CountryImporter>();
			collection.AddSingleton<Iso3166Importer>();

			return collection;
		}
	}
}