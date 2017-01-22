using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DDD.Logging.Mongo
{
    public class ErrorLog
    {
        /// <summary>  
        /// Gets or sets the Error log identifier.  
        /// </summary>  
        /// <value>  
        /// The Error log identifier.  
        /// </value>  
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id
        {
            get; set;
        }
      /// <summary>  
      /// Gets or sets the date.  
      /// </summary>  
      /// <value>  
      /// The date.  
      /// </value>  
        public DateTime Date { get; set; }
        /// <summary>  
        /// Gets or sets the thread.  
        /// </summary>  
        /// <value>  
        /// The thread.  
        /// </value>  
        public string Thread { get; set; }
        /// <summary>  
        /// Gets or sets the level.  
        /// </summary>  
        /// <value>  
        /// The level.  
        /// </value>  
        public string Level { get; set; }
        /// <summary>  
        /// Gets or sets the logger.  
        /// </summary>  
        /// <value>  
        /// The logger.  
        /// </value>  
        public string Logger { get; set; }
        /// <summary>  
        /// Gets or sets the message.  
        /// </summary>  
        /// <value>  
        /// The message.  
        /// </value>  
        public string Message { get; set; }
        /// <summary>  
        /// Gets or sets the exception.  
        /// </summary>  
        /// <value>  
        /// The exception.  
        /// </value>  
        public string Exception { get; set; }
        /// <summary>  
        /// Gets or sets the user agent.  
        /// </summary>  
        /// <value>  
        /// The user agent.  
        /// </value>  
        public string UserAgent { get; set; }
        /// <summary>  
        /// Gets or sets the ip address.  
        /// </summary>  
        /// <value>  
        /// The ip address.  
        /// </value>  
        public string IpAddress { get; set; }
        /// <summary>  
        /// Gets or sets the URL.  
        /// </summary>  
        /// <value>  
        /// The URL.  
        /// </value>  
        public string Url { get; set; }
        /// <summary>  
        /// Gets or sets the referrer.  
        /// </summary>  
        /// <value>  
        /// The referrer.  
        /// </value>  
        public string Referrer { get; set; }
        /// <summary>  
        /// Gets or sets the name of the user.  
        /// </summary>  
        /// <value>  
        /// The name of the user.  
        /// </value>  
        public string UserName { get; set; }
        /// <summary>  
        /// Gets or sets the name of the server.  
        /// </summary>  
        /// <value>  
        /// The name of the server.  
        /// </value>  
        public string ServerName { get; set; }
    }
}
