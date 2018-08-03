using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CSharpOddOrEven
{
    public static class OddOrEven
    {
        [FunctionName("OddOrEven")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")]HttpRequest req, ILogger log)
        {
            log.LogInformation("Odd or even trigger fired - HTTP");

            string numberQueryValue = req.Query["number"];

            if (int.TryParse(numberQueryValue, out int number))
            {
                return new OkObjectResult(number % 2 == 0 ? "Even" : "Odd");
            }
            else
            {
                return new BadRequestObjectResult($"Unable to parse the query parameter 'number'. Got value: {numberQueryValue}");
            }
        }
    }
}
