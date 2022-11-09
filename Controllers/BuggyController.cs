using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound(){
            return NotFound();
        }
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest(){
            return BadRequest(new ProblemDetails{Title="there is a bad request"});
        }       
        [HttpGet("Unauthorized")]
        public ActionResult GetUnauthorized(){
            return Unauthorized("This is Unauthorized request");
        }       
        [HttpGet("validation-error")]
        public ActionResult GetValidationError(){
            ModelState.AddModelError("problem1","there is one problem");
            ModelState.AddModelError("problem2","there is two problem");
            return ValidationProblem();
        }
         [HttpGet("server-error")]
        public ActionResult GetServerError(){
            throw new Exception("There is server error");
        }
    }
}