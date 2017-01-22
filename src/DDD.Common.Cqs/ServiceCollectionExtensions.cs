using DDD.Common.Cqs.Command;
using DDD.Common.Cqs.Query;
using Microsoft.Extensions.DependencyInjection;

namespace DDD.Common.Cqs
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
