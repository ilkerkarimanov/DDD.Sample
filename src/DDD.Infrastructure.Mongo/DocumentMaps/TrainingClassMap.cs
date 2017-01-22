using DDD.Domain.Todos;
using DDD.Infrastructure.Mongo.DocumentMaps;
using MongoDB.Bson.Serialization;

namespace DDD.Infrastructure.Mongo.ClassMaps
{
    public class TodoClassMap : MongoDbClassMap<Todo>
    {
        public override void Map(BsonClassMap<Todo> cm)
        {
            cm.AutoMap();

            //every doc has to have an id
            cm.MapIdField(x => x.Id);
            cm.MapProperty(x => x.State);
            cm.MapProperty(x => x.IsDone);
            cm.SetIgnoreExtraElements(true);


            // will set the element name to "sp" in the stored documents

            //unmap the property.. now we won't save it

        }
    }
}