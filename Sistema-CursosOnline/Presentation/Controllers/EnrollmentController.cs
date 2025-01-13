using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentController(EnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpGet("{courseId}")]
        public async Task<IActionResult> GetEnrollmentStatus(int courseId)
        {
            var studentId = GetStudentId();  
            var isEnrolled = await _enrollmentService.IsEnrolledAsync(new EnrollmentDTO
            {
                StudentId = studentId,
                CourseId = courseId
            });
            return Ok(new { IsEnrolled = isEnrolled });
        }

        
        [HttpPost]
        public async Task<IActionResult> EnrollInCourse([FromBody] EnrollmentDTO enrollmentDTO)
        {
            var studentId = GetStudentId();  
            enrollmentDTO.StudentId = studentId;  
            await _enrollmentService.EnrollAsync(enrollmentDTO);
            return Ok("Inscrição realizada com sucesso.");
        }


        [HttpDelete("{courseId}")]
        public async Task<IActionResult> UnenrollFromCourse(int courseId)
        {
            var studentId = GetStudentId();  
            var enrollmentDto = new EnrollmentDTO
            {
                StudentId = studentId,
                CourseId = courseId
            };
            await _enrollmentService.UnenrollAsync(enrollmentDto);
            return Ok("Desinscrição realizada com sucesso.");
        }

    
        private int GetStudentId()
        {
            return int.Parse(User.FindFirst("StudentId")?.Value);
        }
    }
}
