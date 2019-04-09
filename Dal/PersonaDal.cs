using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using WebApiMongoDB.Connections;
using WebApiMongoDB.SendEmail;

namespace WebApiMongoDB.Models
{
    public class PersonaDal
    {
        static ConectionMongo con = new ConectionMongo();
        static Contact conta = new Contact();

        private readonly IMongoDatabase _database;

        public PersonaDal()
        {
            _database = con.Conectar();

        }
        public IEnumerable<Persona> GetAll()

        {
            var pers = _database.GetCollection<Persona>("Personas")
            .Find(new BsonDocument()).ToListAsync();
            return pers.Result;
        }


        public IEnumerable<Persona> GetOne(string name)
        {
            var persona = _database.GetCollection<Persona>("Personas")
            .Find(d => d.Nombre == name).ToListAsync();
            return persona.Result;
        }




        // delete no anda tengo q ver como se hace ....
        public ActionResult<Persona> DeleteOne(Persona perborr)
        {


            var collectio = _database.GetCollection<Persona>("Personas")
                .FindOneAndDelete(per => per.Nombre == perborr.Nombre && per.Apellido == perborr.Apellido);

            return collectio;

        }



        public async Task InsertOneAsync(Persona per)
        {
            var collectio = _database.GetCollection<Persona>("Personas");
            await collectio.InsertOneAsync(per);
            await conta.NewMessageAsync(per);

        }

        // public async Task UpdateOneAsync(string id, Persona per)
        // {    var per2 = new Persona();

        //     var compaid = per._id.ToString();
        //     var collectio = _database.GetCollection<Persona>("Personas");
        //     await collectio.FindOneAndUpdateAsync(s => s._id.ToString() == id , per );

        // }

    }
}