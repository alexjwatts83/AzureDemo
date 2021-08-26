using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace AzureDemo.Functions
{
	public static class TriggerToServicebus
    {
        [FunctionName("TriggerToServicebus")]
        public static void Run([ServiceBusTrigger("test-queue", Connection = "ServiceBusConnectionString")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
