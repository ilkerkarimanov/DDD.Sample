using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    /// <summary>  
    /// Mongo DB logger options.  
    /// </summary>  
    public class MongoLoggerOption
    {
        /// <summary>  
        /// Gets or sets the filters for logging.  
        /// </summary>  
        /// <value>  
        /// The filters for logging.  
        /// </value>  
        public Dictionary<string, LogLevel> Filters { get; set; }
    }
}
