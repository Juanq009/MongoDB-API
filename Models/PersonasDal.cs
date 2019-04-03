using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace WebApiMongoDB.Models
{
    public class PersonasDal
    {
        private readonly IMongoDatabase _database;

        public PersonasDal()
        {
            _database = Conectar();

        }
        public IEnumerable<Personas> Todos()

        {
            var persona = _database.GetCollection<Personas>("Personas")
            .Find(new BsonDocument()).ToListAsync();
            return persona.Result;
        }
        // no esta andando devuelve []

        public IEnumerable<Personas> Uno(string name)
        {
            var persona = _database.GetCollection<Personas>("Personas")
            .Find(d => d.Nombre == name).ToListAsync();
            return persona.Result;
        }

        private IMongoDatabase Conectar()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var database = cliente.GetDatabase("mgGroupPrueba");
            return database;
        }
    }
}