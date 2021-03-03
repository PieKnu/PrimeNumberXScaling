using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeNumberXScaling.Controllers
{
    [ApiController]
    public class CopyMeToCreateController : ControllerBase
    {

        public CopyMeToCreateController()
        {

        }


        [HttpPost]
        [Route("api/ControllerNameOrDifferent/PostName")]  // use "/{id:int}/" for int & or {someStringName} for string in / as additional URL parameter
        public IActionResult PostName([FromBody] RegistrationModel model) // IF we/u use additional url parameter, u have to add/parse it here as argument/parameter eks: ([FromBody] RegistrationModel model, int id, string someStringName)
        {

        }
    }
}
