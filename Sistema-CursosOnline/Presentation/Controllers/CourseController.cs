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

            await _courseService.AddCourseAsync(courseDto);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseDto.Id }, courseDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDto)
        {
            if (courseDto == null || courseDto.Id != id)
            {
                return BadRequest("Id de curso diferentes");
            }

            var existingCourse = await _courseService.GetCourseByIdAsync(id);
            if(existingCourse == null)
            {
                return NotFound();
            }

            await _courseService.UpdateCourseAsync(courseDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(int id)
        {
            var existingCourse = await _courseService.GetCourseByIdAsync(id);
            if (existingCourse == null)
            {
                return NotFound();  
            }

            await _courseService.DeleteCourseAsync(id);
            return NoContent(); 
        }
    }
}
