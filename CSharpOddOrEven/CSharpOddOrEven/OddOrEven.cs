
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace CSharpOddOrEven
{
    public static class OddOrEven
    {
        [FunctionName("OddOrEven")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Odd or even trigger fired");

            int number = int.Parse(req.Query["number"]);

            return new OkObjectResult(number % 2 == 0 ? "Even" : "Odd");
        }
    }
}
