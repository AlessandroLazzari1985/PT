using System;
using PT.Domain.Enums;

namespace PT.Domain
{
	public class Service
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public int AccountId { get; set; }
		public Account Account { get; set; }
		public decimal Price { get; set; }
		public PeriodType PeriodType { get; set; }
		public DateTime StartDate { get; set; }
	}
}
