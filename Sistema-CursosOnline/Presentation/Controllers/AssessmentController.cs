using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssessmentController : ControllerBase
    {
        private readonly IAssessmentService _assessmentService;

        public AssessmentController(IAssessmentService assessmentService)
        {
            _assessmentService = assessmentService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<AssessmentDTO>>> GetByCourseIdAsync(int courseId)
        {
            var assessments = await _assessmentService.GetByCourseIdAsync(courseId);
            return Ok(assessments);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IEnumerable<AssessmentDTO>>> GetByUserIdAsync(int userId)
        {
            var assessments = await _assessmentService.GetByUserIdAsync(userId);
            return Ok(assessments);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(AssessmentDTO assessmentDto)
        {
            await _assessmentService.AddAsync(assessmentDto);
            return CreatedAtAction(nameof(GetByCourseIdAsync), new { courseId = assessmentDto.CourseId }, assessmentDto);
        }

     
        [HttpPut]
        public async Task<ActionResult> UpdateAsync(AssessmentDTO assessmentDto)
        {
            await _assessmentService.UpdateAsync(assessmentDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _assessmentService.DeleteAsync(id);
            return NoContent();
        }

    }
}
