using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApiMongoDB.Models;

namespace WebApiMongoDB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PersonasControllers : ControllerBase
    {

        // [HttpGet]
        // public IEnumerable<Personas> GetAll()
        // {
        //     PersonasDal dal = new PersonasDal();
        //     var persona = dal.Todos();
        //     return persona;
        // }
    }
}