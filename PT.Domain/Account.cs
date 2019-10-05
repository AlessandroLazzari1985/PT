using System;

namespace PT.Domain
{
	public class Account
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime Start { get; set; }
		public string Link { get; set; }
		public string Notes { get; set; }

		// FK
		public int CompanyId { get; set; }
		// Nav
		public Company Company { get; set; }
	}
}