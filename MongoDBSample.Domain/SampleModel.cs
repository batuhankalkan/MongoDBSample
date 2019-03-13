using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoDBSample.Domain
{
    public class SampleModel : IEntity
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
