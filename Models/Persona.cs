using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiMongoDB.Models
{

    public class Persona
    {
        [BsonId]
        public ObjectId _id { get; set; }

        [BsonElement("nombre")]

        public string Nombre { get; set; }
        [BsonElement("apellido")]

        public string Apellido { get; set; }

        [BsonElement("edad")]

        public string Edad { get; set; }


        public Persona(string nombre, string apellido, string edad)
        {

            this.Nombre = nombre;
            this.Apellido = apellido;
            this.Edad = edad;

        }

    }
}