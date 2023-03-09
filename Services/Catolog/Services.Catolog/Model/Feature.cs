using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Services.Catolog.Model
{
    public class Feature
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string CourseId { get; set; }

        public int Duration { get; set; }    
    }
}
