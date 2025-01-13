using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IRepository
{
    public interface IAssessmentRepository
    {

        Task<IEnumerable<Assessment>> GetByCourseIdAsync(int courseId);
        Task CreateAsync(Assessment assessment);
        Task UpdateAsync(Assessment assessment);
        Task DeleteAsync(int courseId);
    }
}
