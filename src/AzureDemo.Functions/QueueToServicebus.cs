using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace AzureDemo.Functions
{
	public class QueueToServicebus
    {
		public QueueToServicebus(IConfiguration config)
		{

		}
        [FunctionName("QueueToServicebus")]
        public void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
