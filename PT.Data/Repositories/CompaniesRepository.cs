using PT.Domain;
using System.Collections.Generic;
using System.Linq;
using PT.WebApi.Model;

namespace PT.Data.Repositories
{
	public class CompaniesRepository
	{
		private readonly PTContext _context;

		public CompaniesRepository(PTContext context)
		{
			_context = context;
		}

		public List<Company> RealAll()
		{
			return _context.Company.ToList();
		}

		public Company Save(CompanyRaw item)
		{
			var newItem = new Company
			{
				Name = item.Name,
				CountryId = item.CountryId
			};

			_context.Company.Add(newItem);
			_context.SaveChanges();
			return newItem;
		}

		public Company FullUpdate(Company currentItem, Company newItem)
		{
			_context.Entry(currentItem).CurrentValues.SetValues(newItem);
			_context.SaveChanges();
			return currentItem;
		}

		public Company PatchUpdate(int id, CompanyRaw currentItem)
		{
			var result = _context.Company.Single(x => x.Id == id);

			result.Name = currentItem.Name;
			result.CountryId = currentItem.CountryId;

			_context.SaveChanges();

			return result;
		}

		public void Delete(int key)
		{
			var toDelete = _context.Company.Single(x => x.Id == key);

			_context.Company.Remove(toDelete);
			_context.SaveChanges();
		}
	}
}
