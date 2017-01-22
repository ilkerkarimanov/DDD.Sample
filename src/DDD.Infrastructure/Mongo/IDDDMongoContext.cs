using DDD.Domain.Todos;
using MongoDB.Driver;

namespace DDD.Infrastructure.Mongo
{
    public interface IMongoContext {
       
        IMongoCollection<Todo> Todos { get; }

        IMongoDatabase Database { get; }

    }
    
}
