using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class ModuleService : IModuleService
    {
        private readonly IModuleRepository _moduleRepository;

        public ModuleService(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        public async Task CreateModuleAsync(ModuleDTO moduleDto)
        {
            var module = new Module
            {
                CourseId = moduleDto.CourseId,
                Title = moduleDto.Title,
                Description = moduleDto.Description,
                LessonCount = moduleDto.LessonCount
            };

            await _moduleRepository.CreateAsync(module);
        }

        public async Task UpdateModuleAsync(int id, ModuleDTO moduleDto)
        {
            var module = await _moduleRepository.GetByIdAsync(id);
            if (module != null)
            {
                module.Title = moduleDto.Title;
                module.Description = moduleDto.Description;
                module.LessonCount = moduleDto.LessonCount;

                await _moduleRepository.UpdateAsync(module);
            }
        }

        public async Task DeleteModuleAsync(int id)
        {
            await _moduleRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId)
        {
            return await _moduleRepository.GetByCourseIdAsync(courseId);
        }
    }
}
