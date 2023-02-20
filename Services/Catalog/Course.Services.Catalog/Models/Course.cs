using MongoDB.Bson.Serialization.Attributes;
using System;
using MongoDB.Bson;
using FreeCourse.Services.Catalog.Dtos;

namespace FreeCourse.Services.Catalog.Models
{
    public class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }
        public string UserId { get; set; }
        public string Picture { get; set; }
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedTime{ get; set; }
        public  FeatureDto Feature { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }
        // Collectionlara satır olarak diğer tarafları tanımlayabilirken
        // bu kısmı göz ardı etmesi için ignore'u kullanabiliriz.
        [BsonIgnore]
        public CategoryDto Category { get; set; }
        
    }
}
