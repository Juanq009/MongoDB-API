using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDB.Models
{

    public class Personas
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("nombre")]

        public string Nombre { get; set; }
        [BsonElement("apellido")]

        public string Apellido { get; set; }
        [BsonElement("edad")]

        public string Edad { get; set; }

    }
}