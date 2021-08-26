﻿using Microsoft.AspNetCore.Mvc;

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
}
