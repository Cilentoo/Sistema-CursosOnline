using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IModuleService
    {
        Task<IEnumerable<Module>> GetModulesByCourseIdAsync(int courseId);
        Task CreateModuleAsync(ModuleDTO moduleDto);
        Task UpdateModuleAsync(int id, ModuleDTO moduleDto);
        Task DeleteModuleAsync(int id);
    }
}
