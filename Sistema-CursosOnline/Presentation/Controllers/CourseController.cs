using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {

        private readonly CourseService _courseService;

        public CourseController(CourseService courseService)
        {
            _courseService = courseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            var courses = await _courseService.GetAllAsync();
            return Ok(courses);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            return Ok(course);
        }

        [HttpPost]
        //[Authorize(Roles = "Instructor")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDTO courseDTO)
        {
            await _courseService.AddAsync(courseDTO);
            return CreatedAtAction(nameof(GetCourseById), new { id = courseDTO.Id }, courseDTO);
        }

        [HttpPut("{id}")]
       // [Authorize(Roles = "Instructor")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDTO )
        {
            if (id != courseDTO.Id)
                return BadRequest("O ID do curso não corresponde.");

            await _courseService.UpdateAsync(courseDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Roles = "Instructor")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.InactiveCourseAsync(id);
            return NoContent();
        }
    }
}
