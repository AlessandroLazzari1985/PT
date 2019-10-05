using PT.Data;
using PT.Domain;
using PT.Domain.Staging;
using System.Collections.Generic;
using System.Linq;

namespace PT.BatchImport.Importers
{
	public class CountryImporter
	{
		private readonly PTContext _context;

		public CountryImporter(PTContext context)
		{
			_context = context;
		}

		public void SaveCountries(List<CountryStage> countryStages)
		{
			var countries = countryStages.Select(MapToCountry).ToList();

			_context.AddRange(countries);
			_context.SaveChanges();
		}

		private Country MapToCountry(CountryStage countryStage)
		{
			return new Country
			{
				Id = countryStage.Countrycode,
				Name = countryStage.Name,
				Aplha2 = countryStage.Alpha2,
				Aplha3 = countryStage.Alpha3,
				Region = countryStage.Region,
				Subregion = countryStage.Subregion,
			};
		}
	}
}
