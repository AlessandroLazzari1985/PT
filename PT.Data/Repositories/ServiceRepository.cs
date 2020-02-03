using PT.Domain;
using PT.Domain.WebApi;
using System.Collections.Generic;
using System.Linq;

namespace PT.Data.Repositories
{
	public class ServiceRepository
	{
		private readonly PTContext _context;

		public ServiceRepository(PTContext context)
		{
			_context = context;
		}

		public List<Service> RealAll()
		{
			return _context.Service.ToList();
		}

		public Service Save(Service item)
		{
			_context.Service.Add(item);
			_context.SaveChanges();
			return item;
		}

		public Service FullUpdate(Service current, Service newValue)
		{
			_context.Entry(current).CurrentValues.SetValues(newValue);
			_context.SaveChanges();
			return current;
		}

		public Service PatchUpdate(int id, ServiceRaw current)
		{
			var actual = _context.Service.Single(x => x.Id == id);

			if (current.PeriodType != default)
				actual.PeriodType = current.PeriodType;

			if (current.AccountId != default)
				actual.AccountId = current.AccountId;


			if (current.Name != default)
				actual.Name = current.Name;

			if (current.Price != default)
				actual.Price = current.Price;

			if (current.StartDate != default)
				actual.StartDate = current.StartDate;

			_context.SaveChanges();

			return actual;
		}

		public void Delete(int key)
		{
			var toDelete = _context.Account.Single(x => x.Id == key);

			_context.Account.Remove(toDelete);
			_context.SaveChanges();
		}
	}
}
