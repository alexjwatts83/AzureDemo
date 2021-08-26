using Microsoft.AspNetCore.Mvc;

namespace AzureDemo.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class IndexController : ControllerBase
	{
		[HttpGet]
		public IActionResult Index()
		{
			return Ok("Index");
		}
	}
}
