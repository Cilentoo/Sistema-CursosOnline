using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpGet("course/{courseId}")]
        public async Task<ActionResult<IEnumerable<ModuleDTO>>> GetByCourseIdAsync(int courseId)
        {
            var modules = await _moduleService.GetByCourseIdAsync(courseId);
            return Ok(modules);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ModuleDTO>> GetByIdAsync(int id)
        {
            var module = await _moduleService.GetByIdAsync(id);
            return Ok(module);
        }

        [HttpPost]
        public async Task<ActionResult> AddAsync(ModuleDTO moduleDto)
        {
            await _moduleService.AddAsync(moduleDto);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = moduleDto.Id }, moduleDto);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAsync(ModuleDTO moduleDto)
        {
            await _moduleService.UpdateAsync(moduleDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAsync(int id)
        {
            await _moduleService.DeleteAsync(id);
            return NoContent();
        }
    }
}
