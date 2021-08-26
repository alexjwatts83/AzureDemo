using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AzureDemo.Functions
{
    public static class TriggerToServicebus
    {
        [FunctionName("TriggerToServicebus")]
        public static void Run([ServiceBusTrigger("test-queue", Connection = "ServicebusConnection")]string myQueueItem, ILogger log)
        {
            log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
        }
    }
}
