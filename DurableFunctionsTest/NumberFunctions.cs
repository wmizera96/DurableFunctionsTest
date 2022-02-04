using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DurableFunctionsTest
{
    public static class NumberFunctions
    {
        private static Random random = new Random();

        [FunctionName(nameof(NumberFunctions.Sum))]
        public static async Task<double> Sum([ActivityTrigger] IEnumerable<double> values)
        {
            await Task.Delay(random.Next(1000, 5000));
            return values.Sum();
        }

        [FunctionName(nameof(NumberFunctions.Square))]
        public static async Task<double> Square([ActivityTrigger] double value)
        {
            await Task.Delay(random.Next(1000, 5000));

            return value * value;
        }
    }
}
