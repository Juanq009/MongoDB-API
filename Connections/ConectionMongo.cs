using MongoDB.Driver;


namespace WebApiMongoDB.Connections
{
    public class ConectionMongo
    {
        public ConectionMongo()
        {
        }

        public string Url { get; set; }
        public string DbName { get; set; }


        public IMongoDatabase Conectar()

        {
            try
            {
                var cliente = new MongoClient("mongodb://localhost:27017");
                var database = cliente.GetDatabase("mgGroupPrueba");
                return database;
            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}