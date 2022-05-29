using Microsoft.AspNetCore.Mvc;

namespace FitnessPathApp.API.Controllers
{

    public class HealthCheckController : BaseController
	{
		[HttpGet]
		public IActionResult Ping()
		{
			return Ok("Healthy");
		}
	}
}
