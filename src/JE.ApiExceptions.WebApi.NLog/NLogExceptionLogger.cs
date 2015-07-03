using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using NLog;

namespace JE.ApiExceptions.WebApi.NLog
{
    public class NLogExceptionLogger : ExceptionLogger
    {
        private readonly Logger _logger;

        public NLogExceptionLogger()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            _logger.Fatal(context.Exception, "Unhandled exception");
            await base.LogAsync(context, cancellationToken).ConfigureAwait(false);
        }
    }
}
