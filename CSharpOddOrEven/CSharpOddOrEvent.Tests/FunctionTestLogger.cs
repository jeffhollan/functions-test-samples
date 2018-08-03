using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Logging.Abstractions.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpOddOrEven.Tests
{
    class FunctionTestLogger : ILogger
    {
        private IList<string> logs;
        public FunctionTestLogger()
        {
            logs = new List<string>();
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
            logs.Add(formatter(state, exception));
        }

        public IList<string> getLogs()
        {
            return logs;
        }
    }
}
