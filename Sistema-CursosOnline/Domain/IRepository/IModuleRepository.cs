using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IRepository
{
    public interface IModuleRepository
    {
        Task<Module> GetByIdAsync(int id);
        Task<IEnumerable<Module>> GetByCourseIdAsync(int courseId);
        Task CreateAsync (Module module);
        Task UpdateAsync (Module module);
        Task DeleteAsync (int id);
    }
}
