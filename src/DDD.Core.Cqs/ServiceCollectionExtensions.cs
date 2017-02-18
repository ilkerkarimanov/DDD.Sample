using DDD.Core.Cqs.Command;
using DDD.Core.Cqs.Query;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Core.Cqs
{
    public static class ServiceCollectionExtensions
        {

            public static IServiceCollection AddCqs(this IServiceCollection services)
            {
                services.AddTransient<IQueryProcessor, QueryProcessor>();
                services.AddTransient<ICommandDispatcher, CommandDispatcher>();
                return services;
            }
        }
}
