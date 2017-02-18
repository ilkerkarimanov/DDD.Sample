using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DDD.Core.Cqs.Command
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger _logger;

        public CommandDispatcher(ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
        {
            _logger = loggerFactory.CreateLogger<CommandDispatcher>();
            _serviceProvider = serviceProvider;
        }

        public async Task<TReturn> DispatchAsync<TCommand, TReturn>(TCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            _logger.LogDebug("Dispatching command {0}", command);
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            
            var handler = _serviceProvider.GetService<IAsyncCommandHandler<TCommand, TReturn>>();
            var result = await handler.HandleAsync(command).ConfigureAwait(false);

            stopwatch.Stop();
            _logger.LogInformation("Execution time for command {0}: {1}", command, stopwatch.Elapsed.ToString("g"));

            return result;
        }
    }
}