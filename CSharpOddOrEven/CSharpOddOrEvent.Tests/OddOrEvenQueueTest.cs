using CSharpOddOrEven;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CSharpOddOrEven.Tests
{
    public class OddOrEvenQueueTest
    {
        private ITestOutputHelper output;
        private Mock<MockHttpMessageHandler> mockHttpMessageHandler;

        public OddOrEvenQueueTest(ITestOutputHelper output)
        {
            mockHttpMessageHandler = new Mock<MockHttpMessageHandler> { CallBase = true };
            this.output = output;
        }

        [Fact]
        public async Task EvenNumberAsync()
        {
            int number = 2;
            FunctionTestLogger logger = new FunctionTestLogger();

            mockHttpMessageHandler
                .Setup(
                    m => m.Send(It.Is<HttpRequestMessage>(h => h.Content.ReadAsStringAsync().Result.Contains("Even"))))
                .Returns(
                    new HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = new StringContent("Test succeeeded", Encoding.UTF8, "application/json")
                    });

            OddOrEvenQueue.client = new Lazy<HttpClient>(() => { return new HttpClient(mockHttpMessageHandler.Object); });

            await OddOrEvenQueue.RunAsync(number.ToString(), logger);

            var wasEven = (from l in logger.getLogs()
                           where l.Equals("Was even")
                           select l).Any();

            Assert.True(wasEven);
        }

        [Fact]
        public void OddNumber()
        {
            int number = 3;
            
        }

        [Fact]
        public void NonNumbers()
        {
            string nonNumber = "I'm Even";
            
        }

        private async Task<bool> isContentAsync(HttpRequestMessage h, string s)
        {
            string content = await h.Content.ReadAsStringAsync();
            return String.Equals(content, s, StringComparison.OrdinalIgnoreCase);
        }
    }
}
