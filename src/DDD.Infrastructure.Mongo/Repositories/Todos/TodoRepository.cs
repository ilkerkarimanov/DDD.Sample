using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using DDD.Domain.Todos;

namespace DDD.Infrastructure.Mongo.Repositories.Todos
{
    public class TodoRepository : MongoDbBase, ITodoRepository
    {
        public TodoRepository(IMongoContext dbContext) : base(dbContext)
        {
        }


        public async Task Create(Todo entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            await _dbContext.Todos.InsertOneAsync(entity);
        }

        public async Task Delete(Todo entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filter = Builders<Todo>.Filter.Eq(s => s.Id, entity.Id);
            await _dbContext.Todos.DeleteOneAsync(filter);
        }

        public async Task Update(Todo entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filter = Builders<Todo>.Filter.Eq(s => s.Id, entity.Id);
            
            await _dbContext.Todos.ReplaceOneAsync(filter, entity);
        }

    }
}
