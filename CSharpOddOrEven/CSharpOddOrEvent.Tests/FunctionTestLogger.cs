using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit.Abstractions;

namespace CSharpOddOrEven.Tests
{
    class FunctionTestLogger : ILogger
    {
        private IList<string> logs;
        private ITestOutputHelper output;
        public FunctionTestLogger(ITestOutputHelper output)
        {
            logs = new List<string>();
            this.output = output;
        }

        /// <inheritdoc />
        public IDisposable BeginScope<TState>(TState state)
        {
            return NullScope.Instance;
        }

        /// <inheritdoc />
        public bool IsEnabled(LogLevel logLevel)
        {
            return false;
        }

        /// <inheritdoc />
        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = formatter(state, exception);
            logs.Add(message);
            output.WriteLine(message);
        }

        public IList<string> getLogs()
        {
            return logs;
        }
    }
}
