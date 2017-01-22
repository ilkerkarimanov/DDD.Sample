using DDD.Infrastructure.Mongo.DocumentMaps;
using System;
using System.Linq;
using System.Reflection;

namespace DDD.Infrastructure.Mongo
{
    public static class MongoDomainMapsRegistrator
    {
        public static void RegisterDocumentMaps()
        {
            var assembly = Assembly.GetAssembly(typeof(MongoDomainMapsRegistrator));
            
            var classMaps = assembly.GetTypes()
            .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
              t.BaseType.GetGenericTypeDefinition() == typeof(MongoDbClassMap<>));
            
            foreach (var classMap in classMaps)
                Activator.CreateInstance(classMap);
        }
    }
}
