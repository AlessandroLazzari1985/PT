using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PT.BatchImport.Importers;

namespace PT.IntegrationTests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void Iso3166Importer()
		{
			var sut = new Iso3166Importer();

			var countries = sut.ImportCountiesStage(Iso3166.all1);

			foreach (var countryStage in countries)
			{
				Console.WriteLine($@"{countryStage.Countrycode}:  {countryStage.Name} {countryStage.Alpha2}");
			}
		}

		[TestMethod]
		public void ImportCountries()
		{
			var sut = new Iso3166Importer();

			var countries = sut.ImportCountiesStage(Iso3166.all1);

			foreach (var countryStage in countries)
			{
				Console.WriteLine($@"{countryStage.Countrycode}:  {countryStage.Name} {countryStage.Alpha2}");
			}
		}


	}
}
