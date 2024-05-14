using MatrimonioBackend.Models;
using MatrimonioBackend.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace MatrimonioBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentservice studentservice;

        public StudentsController(IStudentservice studentservice)
        {
            this.studentservice = studentservice;
        }


        [HttpGet]
        [EnableQuery]
        public ActionResult<IQueryable<Student>> GetAlStudent() { 
            IQueryable<Student> students = this.studentservice.RetrieveAllStudents();
            return Ok(students);

        }
    }
}
