using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB_API.Connections;
using MongoDB_API.Dal;
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

        private readonly IDalPerson _personadal;

        public ValuesController(IEmailSender emailSender, IDalPerson personaldal)
        {
            _emailSender = emailSender;
            _personadal = personaldal;
        }

        // GET api/values


        [HttpGet]
        public IEnumerable<Persona> GetAll()
        {

            var peras = _personadal.GetAll();
            return peras;
        }


        // GET api/values/Nombre
        [HttpGet("{name}")]
        public Task<Persona> Get(string name)
        {
            // enviar email prueba

            var persona = _personadal.GetOneAsync(name);

            return persona;
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] NewPersonRequest request)

        {
            var mensaje = "Se a agregado un nuevo elemento a la colleccion ";
            if (request.Nombre == "" || request.Apellido == "")
            {

                return BadRequest();
            }


            var pe1 = new Persona();
            // mapear
            pe1.Nombre = request.Nombre;
            pe1.Apellido = request.Apellido;
            pe1.Edad = request.Edad;

            await _personadal.InsertOneAsync(pe1);

            await _emailSender.NewMessageAsync(mensaje, pe1);
            return Ok();

        }

        // PUT api/values/id
        [HttpPut("{id}")]
        public Persona Put(string id, [FromBody] NewPersonRequest request)
        {

            var per = new Persona();

            // mapeo tendria q ver para hacer el autoMapper
            per.Nombre = request.Nombre;
            per.Edad = request.Apellido;
            per.Apellido = request.Edad;

            _personadal.UpdateOne(id, per);

            return per;
        }

        // DELETE api/values/
        [HttpDelete]
        public ActionResult<DelRequest> Delete([FromBody] DelRequest respond)
        {
            var mensaje = "Se a borrado el elemento de la colleccion ";

            if (respond.Nombre == "" || respond.Apellido == "")
            {
                return BadRequest();
            }

            var per = new Persona();
            // mapeo
            per.Nombre = respond.Nombre;
            per.Apellido = respond.Apellido;
            per.Edad = "";

            var personas = _personadal.DeleteOneAsync(per);
            _emailSender.NewMessageAsync(mensaje, per);
            return Ok();
        }

    }
}
