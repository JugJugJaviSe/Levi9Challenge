using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Levi9Challenge.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Levi9Challenge.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService) { _studentService = studentService; }

        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            try
            {
                
                var created = _studentService.Create(student);

                if (created == null)
                {
                    return Conflict(new { message = "A student with this email already exists." });
                }

                return Created("", created);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetById(string id)
        {
            try
            {
                var student = _studentService.GetById(id);

                if(student == null)
                {
                    return NotFound();
                }

                return Ok(student);

            }catch(Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }



    }
}
