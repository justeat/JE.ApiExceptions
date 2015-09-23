using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;
using JustEat.StatsD;

namespace JE.ApiExceptions.StatsD
{
    public class StatsDExceptionLogger : ExceptionLogger
    {
        private readonly IStatsDPublisher _stats;

        public StatsDExceptionLogger(IStatsDPublisher stats)
        {
            _stats = stats;
        }

        public override async Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var bucket = BuildMetricName(context);
            _stats.Increment(bucket);
            await base.LogAsync(context, cancellationToken).ConfigureAwait(false);
        }

        protected virtual string BuildMetricName(ExceptionLoggerContext context)
        {
            return string.Format(CultureInfo.InvariantCulture, "errors.{0}", context.Exception.GetType().Name);
        }
    }
}
