using Microsoft.Extensions.DependencyInjection;
using System;

namespace DDD.Core
{
    public static class ServiceCollectionExtensions
        {
            public static IServiceCollection AddSharedKernel(this IServiceCollection services)
            {
                // Initialization code for production
                DateTimeProvider.Init(() => DateTime.UtcNow);

                // Initialization code for unit tests
                //DateTimeProvider.Init(() => new DateTime(2016, 5, 3));
                return services;
            }
        }
}
