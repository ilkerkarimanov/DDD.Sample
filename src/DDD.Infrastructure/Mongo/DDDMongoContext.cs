using DDD.Domain.Todos;
using MongoDB.Driver;

namespace DDD.Infrastructure.Mongo
{
    public class MongoContext<TConn> : IMongoContext
        where TConn : IMongoConnection
    {
        private IMongoClient _client { get; set; }
        private IMongoDatabase _database { get; set; }

        public IMongoCollection<Todo> Todos
        {
            get
            {
                return _database.GetCollection<Todo>("todos");
            }
        }

        IMongoDatabase IMongoContext.Database
        {
            get
            {
                return _database;
            }
        }

        public MongoContext(TConn conn)
        {
            this.Create(conn);
        }

        private void Create(IMongoConnection mongoConn)
        {
            var url = new MongoUrl(mongoConn.DDDMongoStore());
            this._client = new MongoClient(url.Url);
            this._database = this._client.GetDatabase(url.DatabaseName);
        }
    }
}
