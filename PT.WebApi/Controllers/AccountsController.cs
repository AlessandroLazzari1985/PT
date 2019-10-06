using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using PT.Data.Repositories;
using PT.Domain;
using System.Collections.Generic;
using System.Linq;
using PT.Domain.WebApi;

namespace PT.WebApi.Controllers
{

	public class AccountsController : ODataController
	{
		private readonly AccountsRepository _repository;

		public AccountsController(AccountsRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, EnableQuery, ODataRoute("Accounts")]
		public List<Account> GetAccounts()
		{
			return _repository.RealAll();
		}

		[HttpGet, ODataRoute("Accounts({id})")]
		public ActionResult<Account> GetAccount([FromODataUri] int id)
		{
			var result = _repository.RealAll().SingleOrDefault(x => x.Id == id);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost, ODataRoute("Accounts")]
		public ActionResult<Account> AddAccount(Account account)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);
			
			
			//Validazione lato server dei dati in arrivo

			var result = _repository.Save(account);

			return Ok(result);
		}

		[HttpPut, ODataRoute("Accounts")]
		public ActionResult<Account> PutAccount([FromODataUri] int key, Account account)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);
			
			var result = _repository.RealAll().SingleOrDefault(x => x.Id == key);

			if (result == null)
				return NotFound();

			if(account.Id != key)
				BadRequest(ModelState);

			_repository.FullUpdate(result, account);

			return Ok(result);
		}

		[HttpPatch, ODataRoute("Accounts")]
		public ActionResult<Account> PatchAccount([FromODataUri] int key, [FromBody] AccountRaw accountRaw)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);


			return Ok(accountRaw);
		}

		[HttpDelete, ODataRoute("Accounts")]
		public ActionResult DeleteAccount([FromODataUri] int key)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);

			var result = _repository.RealAll().SingleOrDefault(x => x.Id == key);

			if (result == null)
				return NotFound();

			_repository.Delete(key);
			return Ok(result);
		}
	}
}