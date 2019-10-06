using Microsoft.AspNetCore.Mvc;

namespace PT.WebApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public ActionResult<string> WinAuthenticate()
		{
			return Ok("Welcome");
		}
	}
}
