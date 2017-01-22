using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.OptionsModel;
    using MongoDB.Driver;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    /// <summary>  
    /// Mongo DB logger  
    /// </summary>  
    /// <typeparam name="TLog">The type of the log.</typeparam>  
    /// <seealso cref="Microsoft.Extensions.Logging.ILogger" />  
    public class MongoLogger<TLog> : ILogger
      where TLog : ErrorLog, new()
    {
        /// <summary>  
        /// The indentation  
        /// </summary>  
        private const int Indentation = 2;
        /// <summary>  
        /// The filter for log level  
        /// </summary>  
        private readonly Func<string, LogLevel, bool> Filter;
        /// <summary>  
        /// The logger requester name  
        /// </summary>  
        private readonly string Name;
        /// <summary>  
        /// The services  
        /// </summary>  
        private readonly IServiceProvider Services;
        /// <summary>  
        /// The _mongo database  
        /// </summary>  
        private IMongoDatabase _mongoDb;
        /// <summary>  
        /// Initializes a new instance of the <see cref="MongoLogger{TLog}"/> class.  
        /// </summary>  
        /// <param name="name">The name.</param>  
        /// <param name="filter">The filter.</param>  
        /// <param name="serviceProvider">The service provider.</param>  
        public MongoLogger(string name, Func<string, LogLevel, bool> filter, IServiceProvider serviceProvider)
        {
            Name = name;
            Filter = filter ?? GetFilter(serviceProvider.GetService<IOptions<MongoLoggerOption>>());
            Services = serviceProvider;
        }
        /// <summary>  
        /// Gets the database log.  
        /// </summary>  
        /// <value>  
        /// The database log.  
        /// </value>  
        protected IMongoCollection<ErrorLog> DbLog
        {
            get
            {
                _mongoDb = _mongoDb ?? Services.GetService<Infrastructure.Mongo.IMongoContext>().Database;
                return _mongoDb.GetCollection<ErrorLog>("errorlogs");
            }
        }
        /// <summary>  
        /// Begins a logical operation scope.  
        /// </summary>  
        /// <typeparam name="TState"></typeparam>  
        /// <param name="state">The identifier for the scope.</param>  
        /// <returns>  
        /// An IDisposable that ends the logical operation scope on dispose.  
        /// </returns>  
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        /// <summary>  
        /// Checks if the given <paramref name="logLevel" /> is enabled.  
        /// </summary>  
        /// <param name="logLevel">level to be checked.</param>  
        /// <returns>  
        ///  <c>true</c> if enabled.  
        /// </returns>  
        public bool IsEnabled(LogLevel logLevel)
        {
            return Filter(Name, logLevel);
        }
        /// <summary>  
        /// Writes a log entry.  
        /// </summary>  
        /// <typeparam name="TState"></typeparam>  
        /// <param name="logLevel">Entry will be written on this level.</param>  
        /// <param name="eventId">Id of the event.</param>  
        /// <param name="state">The entry to be written. Can be also an object.</param>  
        /// <param name="exception">The exception related to this entry.</param>  
        /// <param name="formatter">Function to create a <c>string</c> message of the <paramref name="state" /> and <paramref name="exception" />.</param>  
        public async void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
          Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel))
            {
                return;
            }
            var message = string.Empty;
            var values = state as IReadOnlyList<KeyValuePair<string, object>>;
            if (formatter != null)
            {
                message = formatter(state, exception);
            }
            else if (values != null)
            {
                var builder = new StringBuilder();
                FormatLogValues(
                  builder,
                  values,
                  level: 1,
                  bullet: false);
                message = builder.ToString();
                if (exception != null)
                {
                    message += Environment.NewLine + exception;
                }
            }
            else
            {
                message = $"{Convert.ToString(message)} [Check formatting]";
            }
            if (string.IsNullOrEmpty(message))
            {
                return;
            }
            var log = new TLog
            {
                Date = DateTime.UtcNow,
                Level = logLevel.ToString(),
                Logger = Name,
                Message = message,
                Thread = eventId.ToString(),
            };
            if (exception != null)
            {
                log.Exception = exception.ToString();
            }
            var httpContext = Services.GetRequiredService<IHttpContextAccessor>()?.HttpContext;
            if (httpContext != null)
            {
                log.UserAgent = httpContext.Request.Headers["User-Agent"];
                log.UserName = httpContext.User.Identity.Name;
                try
                {
                    log.IpAddress = httpContext.Connection.LocalIpAddress?.ToString();
                }
                catch (ObjectDisposedException)
                {
                    log.IpAddress = "Disposed";
                }
                log.Url = httpContext.Request.Path;
                log.ServerName = httpContext.Request.Host.Value;
                log.Referrer = httpContext.Request.Headers["Referer"];
            }
            await DbLog.InsertOneAsync(log);
        }
        /// <summary>  
        /// Formats the log values.  
        /// </summary>  
        /// <param name="builder">The builder.</param>  
        /// <param name="logValues">The log values.</param>  
        /// <param name="level">The level.</param>  
        /// <param name="bullet">if set to <c>true</c> new item insert.</param>  
        private void FormatLogValues(StringBuilder builder, IReadOnlyList<KeyValuePair<string, object>> logValues, int level, bool bullet)
        {
            if (logValues == null)
            {
                return;
            }
            var isFirst = true;
            foreach (var kvp in logValues)
            {
                builder.AppendLine();
                if (bullet && isFirst)
                {
                    builder.Append(' ', level * Indentation - 1)
                        .Append('-');
                }
                else
                {
                    builder.Append(' ', level * Indentation);
                }
                builder.Append(kvp.Key)
                    .Append(": ");
                if (kvp.Value is IEnumerable && !(kvp.Value is string))
                {
                    foreach (var value in (IEnumerable)kvp.Value)
                    {
                        if (value is IReadOnlyList<KeyValuePair<string, object>>)
                        {
                            FormatLogValues(
                              builder,
                              (IReadOnlyList<KeyValuePair<string, object>>)value,
                              level + 1,
                              bullet: true);
                        }
                        else
                        {
                            builder.AppendLine()
                                .Append(' ', (level + 1) * Indentation)
                                .Append(value);
                        }
                    }
                }
                else if (kvp.Value is IReadOnlyList<KeyValuePair<string, object>>)
                {
                    FormatLogValues(
                      builder,
                      (IReadOnlyList<KeyValuePair<string, object>>)kvp.Value,
                      level + 1,
                      bullet: false);
                }
                else
                {
                    builder.Append(kvp.Value);
                }
                isFirst = false;
            }
        }
        /// <summary>  
        /// Gets the filter.  
        /// </summary>  
        /// <param name="options">The options.</param>  
        /// <returns>Filtered item based on request.</returns>  
        private Func<string, LogLevel, bool> GetFilter(IOptions<MongoLoggerOption> options)
        {
            if (options != null)
            {
                return ((category, level) => GetFilter(options.Value, category, level));
            }
            else
                return ((category, level) => true);
        }
        /// <summary>  
        /// Gets the filter.  
        /// </summary>  
        /// <param name="options">The options.</param>  
        /// <param name="category">The category.</param>  
        /// <param name="level">The level.</param>  
        /// <returns>Filtered item based on request.</returns>  
        private bool GetFilter(MongoLoggerOption options, string category, LogLevel level)
        {
            if (options.Filters != null)
            {
                var filter = options.Filters.Keys.FirstOrDefault(p => category.StartsWith(p));
                if (filter != null)
                    return (int)options.Filters[filter] <= (int)level;
                else return true;
            }
            return true;
        }
    }
}
