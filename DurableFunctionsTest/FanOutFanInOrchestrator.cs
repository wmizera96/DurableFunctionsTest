using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsTest
{
    public static class FanOutFanInOrchestrator
    {
        [FunctionName(nameof(FanOutFanInOrchestrator))]
        public static async Task<double> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var model = context.GetInput<Model>();

            var tasksList = new List<Task<double>>();
            for (int i = 0; i < model.Number; i++)
            {
                tasksList.Add(context.CallActivityAsync<double>(nameof(NumberFunctions.Square), i));
            }

            var multiplicationResults = await Task.WhenAll(tasksList);


            var sum = await context.CallActivityAsync<double>(nameof(NumberFunctions.Sum), multiplicationResults);

            return sum;
        }
    }
}