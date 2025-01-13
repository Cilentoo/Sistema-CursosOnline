using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _AssessmentService;

        public AssessmentController(IAssessmentService AssessmentService)
        {
            _AssessmentService = AssessmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentDTO AssessmentDto)
        {
            await _AssessmentService.CreateAssessmentAsync(AssessmentDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssessment(int id, [FromBody] AssessmentDTO AssessmentDto)
        {
            await _AssessmentService.UpdateAssessmentAsync(id, AssessmentDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            await _AssessmentService.DeleteAssessmentAsync(id);
            return Ok();
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAssessmentsByCourseId(int courseId)
        {
            var Assessments = await _AssessmentService.GetAssessmentsByCourseIdAsync(courseId);
            return Ok(Assessments);
        }
    }
}
