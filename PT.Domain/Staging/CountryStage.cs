using System.Collections.Generic;
using FileHelpers;

namespace PT.Domain.Staging
{
	// https://github.com/lukes/ISO-3166-Countries-with-Regional-Codes/blob/master/all/all.csv

	[IgnoreFirst(1)]
	[DelimitedRecord(",")]
	public class CountryStage
	{
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Name { get; set; }
		public string Alpha2 { get; set; }
		public string Alpha3 { get; set; }
		public int Countrycode { get; set; }
		public string Iso31662 { get; set; }
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Region { get; set; }
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Subregion { get; set; }
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Intermediateregion { get; set; }
		public string Regioncode { get; set; }
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Subregioncode { get; set; }
		[FieldQuoted('"', QuoteMode.OptionalForBoth)]
		public string Intermediateregioncode { get; set; }
	}

	public class CountryStageArray
	{
		public List<CountryStage>  CountryStages { get; set; }
	}
}
