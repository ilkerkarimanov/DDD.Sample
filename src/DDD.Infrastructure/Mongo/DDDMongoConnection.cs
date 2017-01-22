using Microsoft.Extensions.Configuration;
using System;

namespace DDD.Infrastructure.Mongo
{
    public class MongoConnection : IMongoConnection
    {
        private IConfiguration _configuration;

        public MongoConnection(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string DDDMongoStore() => dddMongoStore();

        private string dddMongoStore(string name = "DDDMongoStore")
        {
            if (_configuration == null) throw new ArgumentNullException(nameof(_configuration));

            return _configuration[$"connectionStrings:{name}"];
        }

    }
}
