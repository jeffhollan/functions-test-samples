using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System.Collections.Generic;
using System.Numerics;
using Xunit;

namespace CSharpOddOrEven.Tests
{
    public class OddOrEvenTests
    {
        private readonly ILogger logger = NullLoggerFactory.Instance.CreateLogger("Test");

        [Theory]
        [MemberData(nameof(Numbers.EvenNumbers), MemberType = typeof(Numbers))]
        public void EvenNumber(BigInteger number)
        {
            var request = GenerateHttpRequest(number);
            var response = OddOrEven.Run(request, logger);

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Even", ((OkObjectResult)response).Value as string);
        }

        [Theory]
        [MemberData(nameof(Numbers.OddNumbers), MemberType = typeof(Numbers))]
        public void OddNumber(BigInteger number)
        {
            var request = GenerateHttpRequest(number);
            var response = OddOrEven.Run(request, logger);

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Odd", ((OkObjectResult)response).Value as string);
        }

        [Fact]
        public void NonNumbers()
        {
            string nonNumber = "I'm Even";

            var request = GenerateHttpRequest(nonNumber);
            var response = OddOrEven.Run(request, logger);

            Assert.IsType<BadRequestObjectResult>(response);
            Assert.Contains("Unable to parse", ((BadRequestObjectResult)response).Value as string);
        }

        private DefaultHttpRequest GenerateHttpRequest(object number)
        {
            var request = new DefaultHttpRequest(new DefaultHttpContext());
            var queryParams = new Dictionary<string, StringValues>() { { "number", number.ToString() } };
            request.Query = new QueryCollection(queryParams);
            return request;
        }
    }
}
