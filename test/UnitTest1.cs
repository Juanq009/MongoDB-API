using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB_API.Dal;
using MongoDB_API.SendEmail;
using WebApiMongoDB.Controllers;
using WebApiMongoDB.Models;

namespace MongoDB_API
{
    [TestClass]
    public class ValuesController_Deberia
    {
        [TestMethod]
        public void DevolverTodasLasPersonas()
        {
            // arrange 
            var fakesender = new Fakesender();
            var fakePersonaDal = new FakePersonaDal();
            var controller = new ValuesController(fakesender, fakePersonaDal);
            var per = new NewPersonRequest();
            per.Apellido = "casa";
            per.Nombre = "auto";
            per.Edad = "20";

            // act

            var personas = controller.Post(per);

            // assert

            Assert.AreEqual("auto", fakesender.MensajeEnviado);


        }
    }

    public class Fakesender : IEmailSender
    {
        public string MensajeEnviado { get; set; }
        public async Task NewMessageAsync(string mensaje, Persona per)
        {
            MensajeEnviado = per.Nombre;

        }
    }
    public class FakePersonaDal : IDalPerson
    {

        public Task<Persona> DeleteOneAsync(Persona perborr)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Persona> GetAll()
        {
            return null;
        }

        public Task<Persona> GetOneAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertOneAsync(Persona per)
        {

        }

        public void UpdateOne(string id, Persona per)
        {
            throw new System.NotImplementedException();
        }
    }
}
