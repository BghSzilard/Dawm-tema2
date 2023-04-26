using Core.Dtos;
using Core.Services;
using DataLayer.Dtos;
using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private StudentService studentService { get; set; }
        public StudentsController(StudentService studentService)
        {
            this.studentService = studentService;
        }
        [HttpPost("/register")]
        [AllowAnonymous]
        public IActionResult Register(StudentRegisterDto payload)
        {
            studentService.Register(payload);
            return Ok();
        }
        [HttpPost("/login")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = studentService.Validate(payload);
            return Ok(new { token = jwtToken });
        }

        [HttpGet("/get-all")]
        public ActionResult<List<Student>> GetAll()
        {
            var results = studentService.GetAll();

            return Ok(results);
        }
        [HttpGet("/getOwnGrades")]
        [Authorize(Roles ="Student")]
        public ActionResult<List<Grade>> GetOwnGrades([FromBody] StudentGradesRequest request)
        {
            var result = studentService.GetGradesById(request.StudentId);
            return Ok(result);
        }
    }
}