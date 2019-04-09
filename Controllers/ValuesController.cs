using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using MongoDB_API.Connections;
using MongoDB_API.SendEmail;
using WebApiMongoDB.Models;
using WebApiMongoDB.SendEmail;

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IEmailSender _emailSender;
        private readonly IConnectionDataBase _connection;

        public ValuesController(IEmailSender emailSender, IConnectionDataBase connection)
        {
            _emailSender = emailSender;
            _connection = connection;
        }

        // GET api/values


        [HttpGet]
        public IEnumerable<Persona> GetAll()
        {
            PersonaDal dal = new PersonaDal(_connection);
            var peras = dal.GetAll();
            return peras;
        }


        // GET api/values/Nombre
        [HttpGet("{name}")]
        public IEnumerable<Persona> Get(string name)
        {
            // enviar email prueba


            PersonaDal dal = new PersonaDal(_connection);

            var persona = dal.GetOne(name);

            return persona;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewPersonRequest request)
        {
            if (request.Nombre == "" || request.Apellido == "")
            {

                return BadRequest();
            }

            PersonaDal dal = new PersonaDal(_connection);
            var pe1 = new Persona();



            // mapear
            pe1.Nombre = request.Nombre;
            pe1.Apellido = request.Apellido;
            pe1.Edad = request.Edad;

            await dal.InsertOneAsync(pe1);
            await _emailSender.NewMessageAsync(pe1);


            return Ok();

        }

        // PUT api/values/5
        // [HttpPut("{id}")]
        // public ActionResult<Persona> Put(string id, [FromBody] NewPersonRequest request)
        // {


        //     if (request.Nombre == "" || request.Apellido == "")
        //     {
        //         return BadRequest();
        //     }
        //     var perdal = new PersonaDal();
        //     var per = new Persona();

        //     // mapeo
        //     per.Nombre = request.Nombre;
        //     per.Edad = request.Apellido;
        //     per.Apellido = request.Edad;
        //     perdal.UpdateOne(id, per);

        //     return per;
        // }

        // DELETE api/values/
        [HttpDelete]
        public ActionResult<DelRequest> Delete([FromBody] DelRequest respond)
        {

            if (respond.Nombre == "" || respond.Apellido == "")
            {
                return BadRequest();
            }
            PersonaDal dal = new PersonaDal(_connection);
            var per = new Persona();
            // mapeo
            per.Nombre = respond.Nombre;
            per.Apellido = respond.Apellido;
            per.Edad = "";

            var personas = dal.DeleteOne(per);
            return Ok();
        }

    }
}
