using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using MongoDB_API.Connections;
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
        public void TestEnvioEmail()
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
        [TestMethod]
        public void DeberiaDevolverTodasLasPersonas()
        {
            //  arrange
            var fakepersonadal = new FakePersonaDal();
            var fakesender = new Fakesender();
            var controller = new ValuesController(fakesender, fakepersonadal);
            //act 
            var personas = controller.GetAll();
            //assert 
            // Assert.AreEqual(null, personas);
            // preguntar a franco por test

        }
        [TestMethod]
        public void DeberiaConectarAlaBaseDeDatos()
        {
            // arrange
            var fakeconnection = new FakeConnection();

            // act
            var fakemogno = fakeconnection.Conectar();
            var result = fakeconnection.CheckConnection(fakemogno); // <-- fijarse aca

            // assert
            Assert.AreEqual(true, result);

            // siempre pasa el test, tendria q buscar una forma de demostrar si esta conectado o no en checkConnection

        }

    }
    public class FakeConnection : IConnectionDataBase
    {
        public IMongoDatabase Conectar()
        {
            var cliente = new MongoClient("mongodb://localhost:27017");
            var database = cliente.GetDatabase("mgGroupPrueba");

            return database;

        }
        public bool CheckConnection(IMongoDatabase database)
        {
            try
            {
                if (database is null) { return false; } else { return true; }
            }
            catch (MongoConnectionException e)
            { throw e; }

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
            var persona = new Persona();
            var lista = new List<Persona>();
            persona.Nombre = "nombre1";
            persona.Apellido = " apellido1";
            persona.Edad = "edad1";
            lista.Add(persona);
            return lista;
        }

        public Task<Persona> GetOneAsync(string name)
        {
            throw new System.NotImplementedException();
        }

        public async Task InsertOneAsync(Persona per)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateOne(string id, Persona per)
        {
            throw new System.NotImplementedException();
        }
    }
}
