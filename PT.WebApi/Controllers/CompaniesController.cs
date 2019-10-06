using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using PT.Data.Repositories;
using PT.Domain;
using System.Collections.Generic;
using System.Linq;
using PT.WebApi.Model;

namespace PT.WebApi.Controllers
{
	public class CompaniesController : ODataController
	{
		private readonly CompaniesRepository _repository;

		public CompaniesController(CompaniesRepository repository)
		{
			_repository = repository;
		}

		[HttpGet, EnableQuery, ODataRoute("Companies")]
		public List<Company> GetCompanies()
		{
			return _repository.RealAll();
		}

		[HttpGet, ODataRoute("Companies({id})")]
		public ActionResult<Company> GetCompany([FromODataUri] int id)
		{
			var result = _repository.RealAll().SingleOrDefault(x => x.Id == id);

			if (result == null)
				return NotFound();

			return Ok(result);
		}

		[HttpPost, ODataRoute("Companies")]
		public IActionResult AddAccount([FromBody] CompanyRaw item)
		{
			if (!ModelState.IsValid)
				return BadRequest(ModelState);


			//Validazione lato server dei dati in arrivo

			var result = _repository.Save(item);

			return Created(result);
		}

		[HttpPut, ODataRoute("Companies")]
		public ActionResult<Company> PutAccount([FromODataUri] int key, Company item)
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

		[HttpPatch, ODataRoute("Companies")]
		public ActionResult<Company> PatchAccount([FromODataUri] int key, [FromBody] CompanyRaw item)
		{
			if (!ModelState.IsValid)
				BadRequest(ModelState);

			var result = _repository.PatchUpdate(key, item);

			return Ok(result);
		}

		[HttpDelete, ODataRoute("Companies")]
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