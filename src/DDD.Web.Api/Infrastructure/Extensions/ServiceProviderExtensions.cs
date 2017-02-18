using DDD.Core.Cqs.Command;
using DDD.Core.Cqs.Query;
using System;

namespace DDD.Web.Api.Infrastructure
{
    internal static class ServiceProviderExtensions
    {
        public static IQueryProcessor GetQueryProcessor(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            var service = serviceProvider.GetService(typeof(IQueryProcessor)) as IQueryProcessor;
            if (service == null)
            {
                throw new InvalidOperationException("QueryProcessor not registered in container.");
            }

            return service;
        }

        public static ICommandDispatcher GetCommandDispatcher(this IServiceProvider serviceProvider)
        {
            if (serviceProvider == null) throw new ArgumentNullException(nameof(serviceProvider));

            var service = serviceProvider.GetService(typeof(ICommandDispatcher)) as ICommandDispatcher;
            if (service == null)
            {
                throw new InvalidOperationException("CommandDispatcher not registered in container.");
            }

            return service;
        }
    }
}