using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using MongoDB_API.Connections;

namespace WebApiMongoDB.Connections
{
    public class ConectionMongo : IConnectionDataBase
    {
        private readonly IConfiguration _connection;


        public ConectionMongo(IConfiguration connection)
        {
            _connection = connection;
        }

        public IMongoDatabase Conectar()

        {
            try
            {
                var cliente = new MongoClient(_connection["Url"]);
                var database = cliente.GetDatabase(_connection["DataBaseName"]);

                return database;
            }
            catch (MongoConnectionClosedException e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                throw e;
            }
            catch (System.Exception e)
            {
                Console.WriteLine("Error: {0}", e.Message);
                throw e;
            }


        }
    }
}