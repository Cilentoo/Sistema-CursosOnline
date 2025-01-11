using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task<IEnumerable<ModuleDTO>> GetByCourseIdAsync(int courseId)
        {
            var modules = await _moduleRepository.GetByCourseIdAsync(courseId);
            return modules.Select(m => ModuleDTO.FromEntity(m));
        }

        public async Task<ModuleDTO> GetByIdAsync(int id)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            return ModuleDTO.FromEntity(module);
        }

        public async Task AddAsync(ModuleDTO moduleDto)
        {
            var module = moduleDto.ToEntity();
            await _moduleRepository.AddAsync(module);
        }

        public async Task UpdateAsync(ModuleDTO moduleDto)
        {
            var module = moduleDto.ToEntity();
            await _moduleRepository.UpdateAsync(module);
        }

        public async Task DeleteAsync(int id)
        {
            await _moduleRepository.DeleteAsync(id);
        }
    }
}
