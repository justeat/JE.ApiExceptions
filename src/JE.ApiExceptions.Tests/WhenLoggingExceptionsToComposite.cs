using System.Threading;
using System.Web.Http.ExceptionHandling;
using FakeItEasy;
using FakeItEasy.ExtensionSyntax.Full;
using JE.ApiExceptions.WebApi;
using Xunit;

namespace JE.ApiExceptions.Tests
{
    public class WhenLoggingExceptionsToComposite : WhenExceptionIsThrown<CompositeExceptionLogger>
    {
        private IExceptionLogger _1;
        private IExceptionLogger _2;

        protected override void Given()
        {
            base.Given();
            _1 = A.Fake<IExceptionLogger>();
            _2 = A.Fake<IExceptionLogger>();
        }

        protected override CompositeExceptionLogger CreateSUT()
        {
            return new CompositeExceptionLogger(_1, _2);
        }

        [Fact]
        public async void ShouldCallLogOnBoth()
        {
            _1.CallsTo(x => x.LogAsync(A<ExceptionLoggerContext>._, A<CancellationToken>._)).MustHaveHappened();
            _2.CallsTo(x => x.LogAsync(A<ExceptionLoggerContext>._, A<CancellationToken>._)).MustHaveHappened();
        }
    }
}
