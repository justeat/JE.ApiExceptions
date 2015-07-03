using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.ExceptionHandling;

namespace JE.ApiExceptions.WebApi
{
    /// <remarks>This is an internal class inside WebApi, and copied here so we can use it.</remarks>
    public class CompositeExceptionLogger : IExceptionLogger
    {
        private readonly IExceptionLogger[] _loggers;

        public CompositeExceptionLogger(params IExceptionLogger[] loggers)
            : this((IEnumerable<IExceptionLogger>)loggers)
        {
        }

        public CompositeExceptionLogger(IEnumerable<IExceptionLogger> loggers)
        {
            if (loggers == null)
            {
                throw new ArgumentNullException("loggers");
            }

            _loggers = loggers.ToArray();
        }

        public IEnumerable<IExceptionLogger> Loggers
        {
            get { return _loggers; }
        }

        public Task LogAsync(ExceptionLoggerContext context, CancellationToken cancellationToken)
        {
            var tasks = new List<Task>();

            Contract.Assert(_loggers != null);

            foreach (var logger in _loggers)
            {
                if (logger == null)
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "Instance must not be null ({0})", typeof(IExceptionLogger).Name));
                }

                var task = logger.LogAsync(context, cancellationToken);
                tasks.Add(task);
            }

            return Task.WhenAll(tasks);
        }
    }
}
