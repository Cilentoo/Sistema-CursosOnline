using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IAssessmentService
    {
        Task<IEnumerable<AssessmentDTO>> GetByCourseIdAsync(int courseId);
        Task<IEnumerable<AssessmentDTO>> GetByUserIdAsync(int userId);
        Task AddAsync(AssessmentDTO assessmentDto);
        Task UpdateAsync(AssessmentDTO assessmentDto);
        Task DeleteAsync(int id);

    }
}
