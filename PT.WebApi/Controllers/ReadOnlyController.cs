using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PT.Data;
using PT.Domain;

namespace PT.WebApi.Controllers
{
	public class ReadOnlyController : ODataController
	{
		private readonly PTContext _context;

		public ReadOnlyController(PTContext context)
		{
			_context = context;
		}

		[HttpGet, EnableQuery, ODataRoute("Countries")]
	    public List<Country> GetCountries()
	    {
		    return _context.Country.ToList();
	    }
	}
}