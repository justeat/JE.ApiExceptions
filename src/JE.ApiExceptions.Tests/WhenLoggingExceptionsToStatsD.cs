using System.Text.RegularExpressions;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using JE.ApiExceptions.StatsD;
using JustEat.StatsD;
using Xunit;

namespace JE.ApiExceptions.Tests
{
    public class WhenLoggingExceptionsToStatsD : WhenExceptionIsThrown<StatsDExceptionLogger>
    {
        private IStatsDPublisher _fakeStatsd;

        private Regex _regex;

        protected override void Given()
        {
            _regex = new Regex(@"errors\.ForTestPurposesException");
            _fakeStatsd = A.Fake<IStatsDPublisher>();
            //.Setup(x => x.Increment(It.IsRegex(@"errors\.ForTestPurposesException", RegexOptions.Compiled|RegexOptions.IgnoreCase)));
            base.Given();
        }

        protected override StatsDExceptionLogger CreateSUT()
        {
            return new StatsDExceptionLogger(_fakeStatsd);
        }

        [Fact]
        public void ShouldHaveCountedException()
        {
            _fakeStatsd.CallsTo(x => x.Increment(A<string>.That.Matches(y => _regex.IsMatch(y))));
        }
    }
}
