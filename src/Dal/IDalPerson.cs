using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB_API.Connections;
using WebApiMongoDB.Models;

namespace MongoDB_API.Dal
{
    public interface IDalPerson
    {

        IEnumerable<Persona> GetAll();
        Task<Persona> GetOneAsync(string name);
        Task<Persona> DeleteOneAsync(Persona perborr);

        Task InsertOneAsync(Persona per);

        void UpdateOne(string id, Persona per);

    }
}