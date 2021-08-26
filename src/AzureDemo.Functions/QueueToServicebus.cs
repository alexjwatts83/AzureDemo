using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureDemo.Functions
{
	public class QueueToServicebus
    {
		private readonly string _serviceBusConnectionString;

		public QueueToServicebus(IConfiguration config)
		{
			_serviceBusConnectionString = config.GetConnectionString("ServiceBus");
		}

        [FunctionName("QueueToServicebus")]
        public async Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

			log.LogInformation($"Sending videos to queue at: {DateTime.Now}");
			await using (var client = new ServiceBusClient(_serviceBusConnectionString))
			{
				var sender = client.CreateSender("new-youtubevideo");
				//foreach (var videoId in videos)
				//{
				//	await sender.SendMessageAsync(new ServiceBusMessage(videoId));
				//	await _videoService.SaveYoutubeVideo(videoId);
				//	log.LogInformation($"Queued: {videoId}");
				//}
			}
		}
    }
}
