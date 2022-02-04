using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;

namespace DurableFunctionsTest
{
    public static class HttpStart
    {
        [FunctionName("HttpStart")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "orhestrators/{orchestratorName}")] HttpRequest req,
            [DurableClient] IDurableClient starter, string orchestratorName)
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            try
            {
                var model = JsonConvert.DeserializeObject<Model>(requestBody);

                var instanceId = await starter.StartNewAsync(orchestratorName, model);

                return starter.CreateCheckStatusResponse(req, instanceId);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult( new { ErrorMessage = ex.Message, Request = requestBody });
            }
        }
    }
}
