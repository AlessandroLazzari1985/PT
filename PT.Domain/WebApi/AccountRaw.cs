using System;

namespace PT.Domain.WebApi
{
	public class AccountRaw
	{
		public string Username { get; set; }
		public string Password { get; set; }
		public DateTime Start { get; set; }
		public string Link { get; set; }
		public string Notes { get; set; }
		public int CompanyId { get; set; }
	}
}
