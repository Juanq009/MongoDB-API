using MongoDB.Driver;

namespace WebApiMongoDB.Connections
{
    public class ConectionMongo
    {

        public IMongoDatabase Conectar()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var database = cliente.GetDatabase("mgGroupPrueba");
            return database;
        }
    }
}