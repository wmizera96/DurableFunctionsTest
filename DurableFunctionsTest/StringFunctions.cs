using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using System;
using System.Linq;

namespace DurableFunctionsTest
{
    public static class StringFunctions
    {
        [FunctionName(nameof(StringFunctions.Capitalize))]
        public static Model Capitalize([ActivityTrigger] Model value)
        {
            var newName = value.Name.ToUpperInvariant();
            return new Model(newName);
        }

        [FunctionName(nameof(StringFunctions.AddStars))]
        public static Model AddStars([ActivityTrigger] Model value)
        {
            var newName = $"***{value}###";
            return new Model(newName);

        }

        [FunctionName(nameof(StringFunctions.Reverse))]
        public static Model Reverse([ActivityTrigger] Model value)
        {
            var newName = new string(value.Name.ToCharArray().Reverse().ToArray());
            return new Model(newName);
        }
    }
}
