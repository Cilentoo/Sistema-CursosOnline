using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface ICourseService
    {
        Task<IEnumerable<CourseDTO>> GetAllAsync();

        Task<CourseDTO> GetByIdAsync(int id);

        Task AddCourseAsync(CourseDTO courseDTO);

        Task UpdateCourseAsync(CourseDTO courseDTO);

        TaskInactiveAsync(int courseId);
    }
}
