using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureDemo.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class HomeController : ControllerBase
	{
		[HttpGet]
		public IActionResult Index()
		{
			return Ok("Home");
		}
	}
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
