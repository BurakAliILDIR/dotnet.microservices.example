using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Services.Catolog.Model
{
    internal class Feature
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CourseId { get; set; }

        public int Duration { get; set; }    
    }
}
