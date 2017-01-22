using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Logging;
    using System;

        /// <summary>  
        /// <see cref="GlobalExceptionFilter"/> factory.  
        /// </summary>  
        /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IFilterFactory" />  
        public class GlobalExceptionFilterFactory
          : IFilterFactory
        {
            /// <summary>  
            /// Gets a value that indicates if the result of <see cref="M:Microsoft.AspNetCore.Mvc.Filters.IFilterFactory.CreateInstance(System.IServiceProvider)" />  
            /// can be reused across requests.  
            /// </summary>  
            public bool IsReusable => false;
            /// <summary>  
            /// Creates an instance of the executable filter.  
            /// </summary>  
            /// <param name="serviceProvider">The request <see cref="T:System.IServiceProvider" />.</param>  
            /// <returns>  
            /// An instance of the executable filter.  
            /// </returns>  
            public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
            {
                var loggerFactory = (ILogger<GlobalExceptionFilter>)serviceProvider.GetService(typeof(ILogger<GlobalExceptionFilter>));
                return new GlobalExceptionFilter(loggerFactory);
            }
        }

}
