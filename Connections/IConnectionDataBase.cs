using MongoDB.Driver;

namespace MongoDB_API.Connections
{
    public interface IConnectionDataBase
    {
        IMongoDatabase Conectar();
    }
}