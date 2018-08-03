using Moq;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace CSharpOddOrEven.Tests
{
    public class OddOrEvenQueueTest
    {
        private readonly ITestOutputHelper output;
        private Mock<MockHttpMessageHandler> mockHttpMessageHandler;
        private HttpRequestMessage request;

        public OddOrEvenQueueTest(ITestOutputHelper output)
        {
            mockHttpMessageHandler = new Mock<MockHttpMessageHandler> { CallBase = true };
            this.output = output;

            mockHttpMessageHandler
                .Setup(
                    m => m.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(
                    new HttpResponseMessage
                    {
                        StatusCode = System.Net.HttpStatusCode.OK,
                        Content = null
                    })
                .Callback<HttpRequestMessage>(request => this.request = request);

            OddOrEvenQueue.client = new HttpClient(mockHttpMessageHandler.Object);
        }

        [Fact]
        public async Task EvenNumberAsync()
        {
            int number = 2;
            FunctionTestLogger logger = new FunctionTestLogger(output);

            await OddOrEvenQueue.RunAsync(number.ToString(), logger);

            var wasEven = (from l in logger.getLogs()
                           where l.Equals("Was even")
                           select l).Any();

            Assert.True(wasEven);

            Assert.Equal("Even", await request.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task OddNumberAsync()
        {
            int number = 3;
            FunctionTestLogger logger = new FunctionTestLogger(output);

            await OddOrEvenQueue.RunAsync(number.ToString(), logger);

            var wasOdd = (from l in logger.getLogs()
                           where l.Equals("Was odd")
                           select l).Any();

            Assert.True(wasOdd);

            Assert.Equal("Odd", await request.Content.ReadAsStringAsync());

        }

        [Fact]
        public void NonNumbers()
        {
            string nonNumber = "I'm Even";
            FunctionTestLogger logger = new FunctionTestLogger(output);

            Assert.ThrowsAsync<ArgumentException>(async () => 
                await OddOrEvenQueue.RunAsync(nonNumber, logger)
            );

        }
    }
}
