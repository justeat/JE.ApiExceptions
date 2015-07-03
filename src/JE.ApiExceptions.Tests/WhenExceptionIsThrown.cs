using System.Threading;
using System.Web.Http.ExceptionHandling;

namespace JE.ApiExceptions.Tests
{
    public abstract class WhenExceptionIsThrown<T>
        where T : class, IExceptionLogger
    {
        private ExceptionLoggerContext _context;
        private readonly T SystemUnderTest;

        protected WhenExceptionIsThrown()
        {
            Given();
            SystemUnderTest = CreateSUT();
            When();
        }

        protected virtual void Given()
        {
            _context = GivenContext();
        }

        private ExceptionLoggerContext GivenContext()
        {
            var exceptionContext = new ExceptionContext(
                new ForTestPurposesException(),
                new ExceptionContextCatchBlock("foo", true, true));
            return new ExceptionLoggerContext(exceptionContext);
        }

        protected virtual void When()
        {
            SystemUnderTest.LogAsync(_context, new CancellationToken()).ConfigureAwait(false);
        }

        protected abstract T CreateSUT();
    }
}
