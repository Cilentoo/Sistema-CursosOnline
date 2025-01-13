using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestSharp;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;
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
        //[Authorize(Role = "Instructor")]
        public async Task<IActionResult> AddCourse([FromBody] CourseDTO courseDTO, IFormFile imageFile)
        {
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    courseDTO.PhotoData = memoryStream.ToArray(); 
                }
            }

                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new UnauthorizedAccessException("Usuário não autenticado"));

            
            await _courseService.AddAsync(courseDTO, userId);

            return CreatedAtAction(nameof(GetCourseById), new { id = courseDTO.Id }, courseDTO);
        }

        [HttpPut("{id}")]
        // [Authorize(Role = "Instructor")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] CourseDTO courseDTO, IFormFile imageFile)
        {
            if (id != courseDTO.Id)
                return BadRequest("O ID do curso não corresponde.");

            if (!User.IsInRole("Instructor"))
            {
                return Forbid("Apenas instrutores podem atualizar cursos.");
            }

            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    courseDTO.PhotoData = memoryStream.ToArray(); 
                }
            }

            await _courseService.UpdateAsync(courseDTO);
            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Authorize(Role = "Instructor")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            
            if (!User.IsInRole("Instructor"))
            {
                return Forbid("Apenas instrutores podem deletar cursos.");
            }

            await _courseService.InactiveCourseAsync(id);
            return NoContent();
        }
    }
}
