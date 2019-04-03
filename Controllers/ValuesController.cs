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
        public IEnumerable<Personas> GetAll()
        {
            PersonasDal dal = new PersonasDal();
            var persona = dal.Todos();
            return persona;
        }


        // GET api/values/id
        [HttpGet("{name}")]
        public IEnumerable<Personas> Get(string name)
        {
            PersonasDal dal = new PersonasDal();
            var persona = dal.Uno(name);
            return persona;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
