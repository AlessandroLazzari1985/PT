using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using PT.Data.Repositories;
using PT.Domain;
using PT.Domain.WebApi;

namespace PT.WebApi.Controllers
{
	public class ServiceController : ODataController
	{
		private readonly ServiceRepository _repository;

		public ServiceController(ServiceRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, EnableQuery, ODataRoute("Services")]
		public List<Service> GetAccounts()
		{
			return _repository.RealAll();
		}

		[HttpGet, ODataRoute("Services({id})")]
		public ActionResult<Service> GetAccount([FromODataUri] int id)
		{
			var result = _repository.RealAll().SingleOrDefault(x => x.Id == id);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost, ODataRoute("Services")]
		public ActionResult<Account> AddAccount([FromBody] Service item)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);


			//Validazione lato server dei dati in arrivo

			var result = _repository.Save(item);

			return Ok(result);
		}

		[HttpPut, ODataRoute("Services")]
		public ActionResult<Account> PutAccount([FromODataUri] int key, [FromBody] Service item)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);

			var result = _repository.RealAll().SingleOrDefault(x => x.Id == key);

			if (result == null)
				return NotFound();

			if (item.Id != key)
				BadRequest(ModelState);

			_repository.FullUpdate(result, item);

			return Ok(result);
		}

		[HttpPatch, ODataRoute("Services")]
		public ActionResult<Account> PatchAccount([FromODataUri] int key, [FromBody] ServiceRaw itemRaw)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);


			_repository.PatchUpdate(key, itemRaw);

			return Ok(itemRaw);
		}

		[HttpDelete, ODataRoute("Services")]
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