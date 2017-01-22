using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using MongoDB.Driver;
using System.Collections.Generic;
using DDD.Domain.Todos;

namespace DDD.Infrastructure.Mongo.Repositories.Todos
{
    public class TodoFinder : MongoDbBase, ITodoFinder
    {
        public TodoFinder(IMongoContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Todo>> All(CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            return await Task.Factory.StartNew(() =>
            {
                return _dbContext.Todos.AsQueryable().ToEnumerable();
            }, cancellationToken);
        }

        public async Task<Maybe<Todo>> GetById(TodoId id, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            var filter = Builders<Todo>.Filter.Eq(s => s.Id, id);
            var result = _dbContext.Todos.Find(filter).ToListAsync();
            return await Task.Factory.StartNew(() =>
            {
                var first = result.Result.FirstOrDefault();
                return first;
            }, cancellationToken);
        }

    }
}
