using Levi9Challenge.Models;
using Levi9Challenge.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Levi9Challenge.Controllers
{
    [ApiController]
    [Route("students")]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepo;

        public StudentController(IStudentRepository studentRepo) { _studentRepo = studentRepo; }

        [HttpPost]
        public ActionResult<Student> CreateStudent([FromBody] Student student)
        {
            try
            {
                var created = _studentRepo.Create(student);
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
                var student = _studentRepo.GetById(id);

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
