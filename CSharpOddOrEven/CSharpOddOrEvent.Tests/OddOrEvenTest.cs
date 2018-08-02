using CSharpOddOrEven;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using Xunit;

namespace CSharpOddOrEvent.Tests
{
    public class OddOrEvenTests
    {
        private ILogger logger = NullLoggerFactory.Instance.CreateLogger("Test");

        [Fact]
        public void EvenNumber()
        {
            int number = 2;

            var request = GenerateHttpRequest(number);
            var response = OddOrEven.Run(request, logger);

            Assert.IsType<OkObjectResult>(response);
            Assert.Equal("Even", ((OkObjectResult)response).Value as string);
        }

        [Fact]
        public void OddNumber()
        {
            int number = 3;

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
