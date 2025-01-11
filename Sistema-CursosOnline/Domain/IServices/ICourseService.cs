using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetAllAsync();
        Task<CourseDTO> GetByIdAsync(int id);
        Task<IEnumerable<CourseDTO>> GetByStatusAsync(string status);
        Task AddAsync(CourseDTO courseDto);
        Task UpdateAsync(CourseDTO courseDto);
        Task InactiveCourseAsync(int id);
    }
}
