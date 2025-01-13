using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFrontend")]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAssessment([FromBody] AssessmentDTO AssessmentDto)
        {
            await _assessmentService.CreateAssessmentAsync(AssessmentDto);
            return Ok();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssessment(int id)
        {
            await _assessmentService.DeleteAssessmentAsync(id);
            return Ok();
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetAssessmentsByCourseId(int courseId)
        {
            var Assessments = await _assessmentService.GetAssessmentByCourseIdAsync(courseId);
            return Ok(Assessments);
        }
    }
}
