namespace PT.Domain
{
	public class Company
	{
		public int Id { get; set; }
		public string Name { get; set; }

		// Foreign Keys
		public int CountryId { get; set; }


		// Navigation
		public Country Country { get; set; }
	}
}