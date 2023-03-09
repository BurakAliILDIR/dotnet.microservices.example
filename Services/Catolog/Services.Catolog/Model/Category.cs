using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Services.Catolog.Model
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id  { get; set; }
        public string Name { get; set; }
    }
}
