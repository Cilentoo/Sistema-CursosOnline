using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface ICourseService
    {
        Task<CourseDTO> GetCourseByIdAsync(int id);
        Task<IEnumerable<CourseDTO>> GetAllCoursesAsync();
        Task AddCourseAsync(CourseDTO courseDto);
        Task UpdateCourseAsync(CourseDTO courseDto);
        Task DeleteCourseAsync(int id);
    }
}
