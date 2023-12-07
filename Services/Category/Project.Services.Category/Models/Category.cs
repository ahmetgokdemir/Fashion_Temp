using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Project.Services.Category.Models
{
    public class Category
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string Category_Name { get; set; }
    }
}
