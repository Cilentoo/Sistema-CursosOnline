using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IAssessmentService
    {
        Task CreateAssessmentAsync(AssessmentDTO assessmentDTO);
        Task UpdateAssessmentAsync(int id, AssessmentDTO assessmentDTO);
        Task DeleteAssessmentAsync(int id);

        Task<IEnumerable<Assessment>> GetAssessmentByCourseIdAsync(int courseId);
    }
}
