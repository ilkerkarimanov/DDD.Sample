using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Core.Cqs.Query
{
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public QueryProcessor(ILoggerFactory loggerFactory, IServiceProvider  serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<QueryProcessor>();
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> ProcessAsync<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            _logger.LogDebug($"Processing query {query}");
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var handlerType = typeof(IHandleQueryAsync<,>).MakeGenericType(query.GetType(), typeof(TResult));
            dynamic handler = _serviceProvider.GetService(handlerType);
            if (handler == null)
            {
                throw new NullReferenceException($"Event handler of type IQuery{typeof(TResult)} doesnt exists!");
            }
            var queryResult = await handler.ExecuteAsync((dynamic)query).ConfigureAwait(false);

            stopwatch.Stop();
            _logger.LogInformation($"Execution time for query {query}: {stopwatch.Elapsed.ToString("g")}");
            return queryResult;
        }
    }
}
