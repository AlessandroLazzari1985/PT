using PT.Domain;
using System.Collections.Generic;
using System.Linq;
using PT.Domain.WebApi;
using PT.WebApi.Model;

namespace PT.Data.Repositories
{
	public class AccountsRepository
	{
		private readonly PTContext _context;

		public AccountsRepository(PTContext context)
		{
			_context = context;
		}

		public List<Account> RealAll()
		{
			return _context.Account.ToList();
		}

		public Account Save(Account account)
		{
			_context.Account.Add(account);
			_context.SaveChanges();
			return account;
		}

		public Account FullUpdate(Account currentAccount, Account newAccount)
		{
			_context.Entry(currentAccount).CurrentValues.SetValues(newAccount);
			_context.SaveChanges();
			return currentAccount;
		}

		public Account PatchUpdate(int id, AccountRaw currentItem)
		{
			var actual = _context.Account.Single(x => x.Id == id);

			if (currentItem.CompanyId != default)
				actual.CompanyId = currentItem.CompanyId;

			if (currentItem.Link != default)
				actual.Link = currentItem.Link;


			if (currentItem.Notes != default)
				actual.Notes = currentItem.Notes;

			if (currentItem.Password != default)
				actual.Password = currentItem.Password;

			if (currentItem.Start != default)
				actual.Start = currentItem.Start;

			if (currentItem.Username != default)
				actual.Username = currentItem.Username;

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
