using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDB.Models
{
    public class NewPersonRequest
    {
        [BsonElement("nombre")]
        public string Nombre { get; set; }

        [BsonElement("apellido")]

        public string Apellido { get; set; }
        [BsonElement("edad")]

        public string Edad { get; set; }



    }
}