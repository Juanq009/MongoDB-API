using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_API.Connections;
using Newtonsoft.Json;
using WebApiMongoDB.Connections;
using WebApiMongoDB.SendEmail;

namespace WebApiMongoDB.Models
{
    public class PersonaDal
    {
        private readonly IConnectionDataBase _connection;
        private readonly IMongoDatabase _database;

        public PersonaDal(IConnectionDataBase connection)
        {
            _connection = connection;
            _database = _connection.Conectar();

        }
        public IEnumerable<Persona> GetAll()

        {
            var pers = _database.GetCollection<Persona>("Personas")
            .Find(new BsonDocument()).ToListAsync();
            return pers.Result;
        }


        public async Task<Persona> GetOneAsync(string name)
        {
            var persona = _database.GetCollection<Persona>("Personas");
            var cursor = await persona.FindAsync(d => d.Nombre == name);
            return await cursor.FirstOrDefaultAsync();

        }


        public async Task<Persona> DeleteOneAsync(Persona perborr)
        {


            var collectio = _database.GetCollection<Persona>("Personas");
            var retorno = await collectio.FindOneAndDeleteAsync(per => per.Nombre == perborr.Nombre && per.Apellido == perborr.Apellido);

            return retorno;

        }



        public async Task InsertOneAsync(Persona per)
        {
            var collectio = _database.GetCollection<Persona>("Personas");
            await collectio.InsertOneAsync(per);

        }

        public void UpdateOne(string id, Persona per)
        {

            try
            {
                var collection = _database.GetCollection<Persona>("Personas");
                // collection.FindOneAndReplace(s => s.Nombre == "Franco", per);
            }
            catch (System.Exception error)
            {

                throw error;
            }


        }
    }
}