using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IRepository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetAllAsync();

        Task<Course> GetByIdAsync(int id);

        Task<IEnumerable<Course>> GetByStatusAsync(string status);

        Task AddOrUpdateAsync(Course course);
        
        Task InactiveCourseAsync(int id);
    }
}
