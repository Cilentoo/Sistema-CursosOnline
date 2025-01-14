using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDTO>>> GetAllCourses() 
        {
            var courses = await _courseService.GetAllCoursesAsync();
            return Ok(courses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CourseDTO>> GetCourseById(int id)
        {
            var course = await _courseService.GetCourseByIdAsync(id);
            if (course == null) 
            {
                return NotFound();
            }

            return Ok(course);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCourse([FromBody] CourseDTO courseDto)
        {
            if (courseDto == null)
            {
                return BadRequest("Dados inválidos");
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }

            byte[] coverImageBytes = null;

            if (!string.IsNullOrEmpty(courseDto.CoverImage))
            {
                coverImageBytes = Convert.FromBase64String(courseDto.CoverImage);
            }

            courseDto.CoverImage = coverImageBytes != null ? Convert.ToBase64String(coverImageBytes) : null;

            courseDto.InstructorId = int.Parse(userId);

            await _courseService.AddCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseDto.Id }, courseDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDto)
        {
            if (courseDto == null || id <= 0 || courseDto.Id != id)
            {
                return BadRequest(new { error = "Dados inválidos para atualização." });
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var courseInstructorId = courseDto.InstructorId;

            Debug.WriteLine($"UserId: {userId}, CourseInstructorId: {courseInstructorId}");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("Usuário não autenticado.");
            }


            if (int.Parse(userId) != courseDto.InstructorId)
            {
                return Forbid(JwtBearerDefaults.AuthenticationScheme);
            }

            try
            {
                await _courseService.UpdateCourseAsync(courseDto);
                return Ok(new { message = "Curso atualizado com sucesso." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var existingCourse = await _courseService.GetCourseByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound();  
            }

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId) || existingCourse.InstructorId != int.Parse(userId))
            {
                return Forbid("Usuário não autorizado para deletar este curso.");
            }

            await _courseService.DeleteCourseAsync(id);
            return NoContent(); 
        }
    }
}
