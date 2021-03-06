using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsTest
{
    public static class ChainingOrchestrator
    {
        [FunctionName(nameof(ChainingOrchestrator))]
        public static async Task<Model> RunOrchestrator(
            [OrchestrationTrigger] IDurableOrchestrationContext context)
        {

            var input = context.GetInput<Model>();

            var output1 = await context.CallActivityAsync<Model>(nameof(StringFunctions.Capitalize), input);
            var output2 = await context.CallActivityAsync<Model>(nameof(StringFunctions.AddStars), output1);
            var output3 = await context.CallActivityAsync<Model>(nameof(StringFunctions.Reverse), output2);
            var output4 = await context.CallActivityAsync<Model>(nameof(StringFunctions.AddStars), output3);

            return output4;
        }
    }
}