using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

using WebApiMongoDB.Models;

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values

        [HttpGet]
        public IEnumerable<Persona> GetAll()
        {
            PersonaDal dal = new PersonaDal();
            var peras = dal.GetAllPerson();
            return peras;
        }


        // GET api/values/Nombre
        [HttpGet("{name}")]
        public IEnumerable<Persona> Get(string name)
        {
            PersonaDal dal = new PersonaDal();
            var persona = dal.GetOnePerson(name);
            return persona;
        }

        // POST api/values
        [HttpPost]
        // public IActionResult Post([FromBody] Personas per)
        // {
        //     PersonasDal dal = new PersonasDal();
        //     var persona = dal.PostNewPerson(per);
        //     return Ok();

        // }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        // DELETE api/values/name
        [HttpDelete("{name}")]
        public void Delete([FromBody] string name)
        {
            PersonaDal dal = new PersonaDal();
            var personas = dal.DeleteOnePerson(name);

        }
    }
}
