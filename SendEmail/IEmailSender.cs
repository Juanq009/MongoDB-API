using System.Threading.Tasks;
using WebApiMongoDB.Models;

namespace MongoDB_API.SendEmail
{
    public interface IEmailSender
    {
        Task NewMessageAsync(string mensaje, Persona per);
    }
}