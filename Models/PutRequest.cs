using MongoDB.Bson.Serialization.Attributes;

namespace MongoDB_API.Models
{
    public class PutRequest
    {
        [BsonId]
        public string _id { get; set; }

        [BsonElement("nombre")]

        public string Nombre { get; set; }
        [BsonElement("apellido")]

        public string Apellido { get; set; }

        [BsonElement("edad")]

        public string Edad { get; set; }
    }
}