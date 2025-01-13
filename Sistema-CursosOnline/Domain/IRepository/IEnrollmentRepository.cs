using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Domain.IRepository
{
    public interface IEnrollmentRepository
    {
        Task CreateAsync(Enrollment enrollment);
        Task<Enrollment> GetByStudentAndCourseIdAsync(int studentId, int courseId);
        Task<IEnumerable<Enrollment>> GetByStudentIdAsync(int studentId);
        Task RemoveAsync(int enrollmentId);
    }
}
