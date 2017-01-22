using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    using Microsoft.Extensions.Logging;
    using System;
    /// <summary>  
    /// Mongo DB logger registration.  
    /// </summary>  
    public static class MongoLoggerFactoryExtensions
    {
        /// <summary>  
        /// Adds the Mongo DB logger framework.  
        /// </summary>  
        /// <typeparam name="TLog">The type of the log.</typeparam>  
        /// <param name="factory">The factory.</param>  
        /// <param name="serviceProvider">The service provider.</param>  
        /// <param name="filter">The filter.</param>  
        /// <returns></returns>  
        /// <exception cref="ArgumentNullException"></exception>  
        public static ILoggerFactory AddMongoLogging<TLog>(this ILoggerFactory factory,
          IServiceProvider serviceProvider, Func<string, LogLevel, bool> filter = null)
          where TLog : ErrorLog, new()
        {
            if (factory == null)
            {
                throw new ArgumentNullException(nameof(factory));
            }
            factory.AddProvider(new MongoLoggerProvider<TLog>(serviceProvider, filter));
            return factory;
        }
    }
}
