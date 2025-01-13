using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowFrontend")]
    public class ModuleController : ControllerBase
    {
        private readonly IModuleService _moduleService;

        public ModuleController(IModuleService moduleService)
        {
            _moduleService = moduleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateModule([FromBody] ModuleDTO moduleDto)
        {
            await _moduleService.CreateModuleAsync(moduleDto);
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateModule(int id, [FromBody] ModuleDTO moduleDto)
        {
            await _moduleService.UpdateModuleAsync(id, moduleDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteModule(int id)
        {
            await _moduleService.DeleteModuleAsync(id);
            return Ok();
        }

        [HttpGet("course/{courseId}")]
        public async Task<IActionResult> GetModulesByCourseId(int courseId)
        {
            var modules = await _moduleService.GetModulesByCourseIdAsync(courseId);
            return Ok(modules);
        }
    }
}
