using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using WebApiMongoDB.Connections;
namespace WebApiMongoDB.Models
{
    public class PersonaDal
    {
        static ConectionMongo con = new ConectionMongo();
        private readonly IMongoDatabase _database;

        public PersonaDal()
        {
            _database = con.Conectar();

        }
        public IEnumerable<Persona> GetAllPerson()

        {
            var pers = _database.GetCollection<Persona>("Personas")
            .Find(new BsonDocument()).ToListAsync();
            return pers.Result;
        }


        public IEnumerable<Persona> GetOnePerson(string name)
        {
            var persona = _database.GetCollection<Persona>("Personas")
            .Find(d => d.Nombre == name).ToListAsync();
            return persona.Result;
        }




        // delete no anda tengo q ver como se hace ....
        public ActionResult<Persona> DeleteOnePerson(string firstname)
        {


            var collectio = _database.GetCollection<Persona>("Personas")
                .FindOneAndDelete(per => per.Nombre == firstname);

            return collectio;

        }



        public ActionResult<NewPersonRequest> PostNewPerson(NewPersonRequest per)
        {
            var collectio = _database.GetCollection<Persona>("Personas");
            // collectio.InsertOne(per);


            return per;
        }

    }
}