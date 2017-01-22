using DDD.Domain.Todos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace DDD.Infrastructure.Mongo
{
    public static class MongoDomainPersistanceExtensions
    {

        public static IServiceCollection AddMongoDomainPersistance(this IServiceCollection services, IConfiguration config)
        {
            MongoDomainMapsRegistrator.RegisterDocumentMaps();

            services.Scan(scan => scan
                .FromAssembliesOf(typeof(MongoDomainPersistanceExtensions))
                .AddClasses(classes => classes.AssignableToAny(typeof(ITodoFinder)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(classes => classes.AssignableToAny(typeof(ITodoRepository)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                );

            return services;
        }
    }
}
