using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_API.Connections;
using MongoDB_API.Dal;
using Newtonsoft.Json;
using WebApiMongoDB.Connections;
using WebApiMongoDB.SendEmail;

namespace WebApiMongoDB.Models
{
    public class PersonaDal : IDalPerson
    {
        private readonly IConfiguration _configuracion;

        private readonly IMongoDatabase _database;

        public PersonaDal(IConnectionDataBase connection)
        {

            _database = connection.Conectar();

        }
        public IEnumerable<Persona> GetAll()

        {
            try
            {
                var pers = _database.GetCollection<Persona>("Personas")
                       .Find(new BsonDocument()).ToListAsync();
                return pers.Result;

            }
            catch (System.AggregateException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;

            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;
            }

        }


        public async Task<Persona> GetOneAsync(string name)
        {
            try
            {
                var persona = _database.GetCollection<Persona>("Personas");
                var cursor = await persona.FindAsync(d => d.Nombre == name);
                return await cursor.FirstOrDefaultAsync();
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;
            }


        }


        public async Task<Persona> DeleteOneAsync(Persona perborr)
        {


            try
            {
                var collectio = _database.GetCollection<Persona>("Personas");
                var retorno = await collectio.FindOneAndDeleteAsync(per => per.Nombre == perborr.Nombre && per.Apellido == perborr.Apellido);

                return retorno;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;
            }

        }



        public async Task InsertOneAsync(Persona per)
        {
            try
            {
                var collectio = _database.GetCollection<Persona>("Personas");
                await collectio.InsertOneAsync(per);

            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;
            }
        }

        public void UpdateOne(string id, Persona per)
        {

            try
            {
                var collection = _database.GetCollection<Persona>("Personas");
                // collection.FindOneAndReplace(s => s.Nombre == "Franco", per);
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                Console.WriteLine("Stack trace {0}", e.StackTrace);

                throw e;
            }



        }
    }
}