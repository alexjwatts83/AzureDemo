using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace AzureDemo.Functions
{
	public class TriggerToServicebus
    {
		public TriggerToServicebus()
		{

		}
        [FunctionName("TriggerToServicebus")]
        public async Task Run([ServiceBusTrigger("test-queue", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
