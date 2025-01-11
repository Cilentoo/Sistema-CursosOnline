using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/enrollments")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        private readonly EnrollmentService _enrollmentService;

        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{courseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> GetEnrollmentStatus(int courseId)
        {
            var studentId = GetStudentId(); 
            var isEnrolled = await _enrollmentService.IsEnrolledAsync(courseId, studentId);
            return Ok(new { IsEnrolled = isEnrolled });
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> EnrollInCourse([FromBody] EnrollmentDTO enrollmentDTO)
        {
            var studentId = GetStudentId(); 
            await _enrollmentService.EnrollAsync(enrollmentDTO.CourseId, studentId);
            return Ok("Inscrição realizada com sucesso.");
        }


        [HttpDelete("{courseId}")]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> UnenrollFromCourse(int courseId)
        {
            var studentId = GetStudentId();
            await _enrollmentService.UnenrollAsync(courseId, studentId);
            return Ok("Desinscrição realizada com sucesso.");
        }
        private int GetStudentId()
        {
            return int.Parse(User.FindFirst("StudentId")?.Value);
        }
    }
}
