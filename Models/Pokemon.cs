
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace pokedex.Models
{
    public class Pokemon
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] // Ensures MongoDB expects a valid ObjectId
        public string Id { get; set; } = string.Empty; // Ensure a default value for non-nullable property
        
        [BsonElement("name")] // Optional: Map the property to a specific field in MongoDB
        public string Name { get; set; } = string.Empty; // Ensure a default value for non-nullable property
        
        [BsonElement("type")]
        public string Type { get; set; } = string.Empty; // Ensure a default value for non-nullable property
        
        [BsonElement("level")]
        public int Level { get; set; } // No default needed as int is non-nullable by default
    }
}