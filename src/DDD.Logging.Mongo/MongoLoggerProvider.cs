using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    using Microsoft.Extensions.Logging;
    using System;
    /// <summary>  
    /// Mongo DB logger provider  
    /// </summary>  
    /// <typeparam name="TLog">The type of the log.</typeparam>  
    /// <seealso cref="Microsoft.Extensions.Logging.ILoggerProvider" />  
    public class MongoLoggerProvider<TLog> : ILoggerProvider
      where TLog : ErrorLog, new()
    {
        /// <summary>  
        /// The filter for logging.  
        /// </summary>  
        private readonly Func<string, LogLevel, bool> Filter;
        /// <summary>  
        /// The service provider  
        /// </summary>  
        private readonly IServiceProvider ServiceProvider;
        /// <summary>  
        /// Initializes a new instance of the <see cref="MongoLoggerProvider{TLog}"/> class.  
        /// </summary>  
        /// <param name="serviceProvider">The service provider.</param>  
        /// <param name="filter">The filter.</param>  
        public MongoLoggerProvider(IServiceProvider serviceProvider, Func<string, LogLevel, bool> filter)
        {
            Filter = filter;
            ServiceProvider = serviceProvider;
        }
        /// <summary>  
        /// Creates the logger.  
        /// </summary>  
        /// <param name="name">The name.</param>  
        /// <returns></returns>  
        public ILogger CreateLogger(string name)
        {
            return new MongoLogger<TLog>(name, Filter, ServiceProvider);
        }
        /// <summary>  
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.  
        /// </summary>  
        public void Dispose()
        {
            // Nothing to dispose.  
        }
    }
}
