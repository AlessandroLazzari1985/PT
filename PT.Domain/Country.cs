using System.ComponentModel.DataAnnotations.Schema;

namespace PT.Domain
{
	public class Country
	{
		[DatabaseGenerated(DatabaseGeneratedOption.None)]
		public int Id { get; set; }
		public string Aplha2 { get; set; }
		public string Aplha3 { get; set; }
		public string Name { get; set; }
		public string Region { get; set; }
		public string Subregion { get; set; }
	}
}