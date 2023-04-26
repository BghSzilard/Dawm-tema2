using Core.Dtos;
using Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    [ApiController]
    [Route("api/teachers")]
    public class TeachersController : ControllerBase
    {
        private TeacherService teacherService { get; set; }
        public TeachersController(TeacherService teacherService)
        {
            this.teacherService = teacherService;
        }
        [HttpPost("/registerTeacher")]
        [AllowAnonymous]
        public IActionResult Register(TeacherRegisterDto payload)
        {
            teacherService.Register(payload);
            return Ok();
        }
        [HttpPost("/loginTeacher")]
        [AllowAnonymous]
        public IActionResult Login(LoginDto payload)
        {
            var jwtToken = teacherService.Validate(payload);
            return Ok(new { token = jwtToken });
        }
    }
}