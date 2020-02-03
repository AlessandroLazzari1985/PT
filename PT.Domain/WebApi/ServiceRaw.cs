using PT.Domain.Enums;
using System;

namespace PT.Domain.WebApi
{
	public class ServiceRaw
	{
		public string Name { get; set; }
		public int AccountId { get; set; }
		public decimal Price { get; set; }
		public PeriodType PeriodType { get; set; }
		public DateTime StartDate { get; set; }
	}
}
