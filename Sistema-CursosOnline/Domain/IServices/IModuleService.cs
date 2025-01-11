using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IModuleService
    {
        Task<IEnumerable<ModuleDTO>> GetByCourseIdAsync(int courseId);
        Task<ModuleDTO> GetByIdAsync(int id);
        Task AddAsync(ModuleDTO moduleDto);
        Task UpdateAsync(ModuleDTO moduleDto);
        Task DeleteAsync(int id);
    }
}
