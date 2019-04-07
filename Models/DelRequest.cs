using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDB.Models
{
    public class DelRequest
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }
        [BsonElement("apellido")]
        public string Apellido { get; set; }
    }
}