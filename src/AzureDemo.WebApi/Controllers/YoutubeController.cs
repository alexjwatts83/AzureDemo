using Google.Apis.Auth.AspNetCore3;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace AzureDemo.WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class YoutubeController : ControllerBase
	{
		[HttpGet("{videoId}")]
		[GoogleScopedAuthorize(YouTubeService.ScopeConstants.Youtube)]
		public async Task<IActionResult> Index([FromServices] IGoogleAuthProvider auth, string videoId)
		{
			GoogleCredential cred = await auth.GetCredentialAsync();
			var service = new YouTubeService(new BaseClientService.Initializer
			{
				HttpClientInitializer = cred
			});

			var request = service.Videos.List("snippet");
			request.Id = videoId;
			var results = await request.ExecuteAsync();
			Console.WriteLine("========== Get Video ==========");
			foreach (var item in results.Items)
			{
				Console.WriteLine($"{item.Snippet.Title}: {item.Id}");
			}

			return Ok("");
		}
	}
}
