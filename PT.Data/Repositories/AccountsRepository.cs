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

		public Company PatchUpdate(int id, AccountRaw currentItem)
		{
			var result = _context.Company.Single(x => x.Id == id);


			_context.SaveChanges();

			return result;
		}

		public void Delete(int key)
		{
			var toDelete = _context.Account.Single(x => x.Id == key);

			_context.Account.Remove(toDelete);
			_context.SaveChanges();
		}
	}
}
