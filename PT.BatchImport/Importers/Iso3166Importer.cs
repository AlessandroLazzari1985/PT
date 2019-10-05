using FileHelpers;
using PT.Domain.Staging;
using System.Collections.Generic;

namespace PT.BatchImport.Importers
{
	public class Iso3166Importer
	{
		public List<CountryStage> ImportCountiesStage(string source)
		{
			var result = new List<CountryStage>();
			var engine = new FileHelperAsyncEngine<CountryStage>();

			using (engine.BeginReadString(source))
			{
				foreach (CountryStage countryStage in engine)
				{
					result.Add(countryStage);
				}
			}

			return result;

		}
	}
}