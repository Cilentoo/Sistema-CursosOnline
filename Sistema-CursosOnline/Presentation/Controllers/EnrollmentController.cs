using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly IEnrollmentService _enrollmentService;

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        [HttpPost("enroll")]
        public async Task<IActionResult> EnrollStudentAsync([FromBody] EnrollmentDTO enrollmentDto)
        {
            try
            {
                var isEnrolled = await _enrollmentService.IsEnrolledAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
                if (isEnrolled)
                    return BadRequest("O aluno já está inscrito neste curso.");

                await _enrollmentService.EnrollStudentAsync(enrollmentDto);
                return Ok("Inscrição realizada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao realizar a inscrição: {ex.Message}");
            }
        }

        [HttpPost("unenroll")]
        public async Task<IActionResult> UnenrollStudentAsync([FromBody] EnrollmentDTO enrollmentDto)
        {
            try
            {
                var isEnrolled = await _enrollmentService.IsEnrolledAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
                if (!isEnrolled)
                    return BadRequest("O aluno não está inscrito neste curso.");

                await _enrollmentService.UnenrollStudentAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
                return Ok("Inscrição cancelada com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cancelar a inscrição: {ex.Message}");
            }
        }

        [HttpGet("check-enrollment/{studentId}/{courseId}")]
        public async Task<IActionResult> CheckEnrollmentAsync(int studentId, int courseId)
        {
            try
            {
                var isEnrolled = await _enrollmentService.IsEnrolledAsync(studentId, courseId);
                return Ok(isEnrolled);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao verificar inscrição: {ex.Message}");
            }
        }
    }
}
