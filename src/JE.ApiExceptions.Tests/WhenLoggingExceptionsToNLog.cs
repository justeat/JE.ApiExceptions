using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using JE.ApiExceptions.WebApi.NLog;
using NLog;
using NLog.Targets;
using Shouldly;
using Xunit;

namespace JE.ApiExceptions.Tests
{
    public class WhenLoggingExceptionsToNLog : WhenExceptionIsThrown<NLogExceptionLogger>
    {
        [Fact]
        public void FatalExceptionWasLogged()
        {
            Logs().ShouldContain(x => x.Contains("FATAL"));
        }

        private static IEnumerable<string> Logs()
        {
            var memoryTarget = LogManager.Configuration.AllTargets.Single(x => x is MemoryTarget) as MemoryTarget;
            Debug.Assert(memoryTarget != null, "memoryTarget != null");
            return memoryTarget.Logs;
        }

        protected override NLogExceptionLogger CreateSUT()
        {
            return new NLogExceptionLogger();
        }
    }
}
