using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace DurableFunctionsTest
{
    public static class FanOutFanInOrchestrator
    {
        [FunctionName(nameof(FanOutFanInOrchestrator))]
        public static async Task<List<string>> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var outputs = new List<string>();

            // Replace "hello" with the name of your Durable Activity Function.
            outputs.Add(await context.CallActivityAsync<string>("FanOutFanInOrchestrator_Hello", "Tokyo"));
            outputs.Add(await context.CallActivityAsync<string>("FanOutFanInOrchestrator_Hello", "Seattle"));
            outputs.Add(await context.CallActivityAsync<string>("FanOutFanInOrchestrator_Hello", "London"));

            // returns ["Hello Tokyo!", "Hello Seattle!", "Hello London!"]
            return outputs;
        }
    }
}