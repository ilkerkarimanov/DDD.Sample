namespace DDD.Logging.Mongo
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.AspNetCore.Routing;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Primitives;
    using Newtonsoft.Json;
    using System;
    using System.Net;
    using System.Threading.Tasks;

    /// <summary>  
    /// Global exception filter for the application.  
    /// </summary>  
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Filters.IExceptionFilter" />  
    public class GlobalExceptionFilter
      : IExceptionFilter
    {
        /// <summary>  
        /// The logger  
        /// </summary>  
        private readonly ILogger<GlobalExceptionFilter> Logger;
        /// <summary>  
        /// Initializes a new instance of the <see cref="GlobalExceptionFilter"/> class.  
        /// </summary>  
        /// <param name="logger">The logger.</param>  
        public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
        {
            Logger = logger;
        }
        /// <summary>  
        /// Called after an action has thrown an <see cref="T:System.Exception" />.  
        /// </summary>  
        /// <param name="context">The <see cref="T:Microsoft.AspNetCore.Mvc.Filters.ExceptionContext" />.</param>  
        public void OnException(ExceptionContext context)
        {
            Logger.LogError(new EventId(999, "GlobalException"), context.Exception, "Unhandled Error");
            context.Result = new ContentResult
            {
                Content = JsonConvert.SerializeObject(new
                {
                    Status = "Failed",
                    Error = "server_error",
                    Message = "Internal server error"
                }),
                ContentType = "application/json",
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }
        
    }
}
