using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IEnrollmentService
    {
        Task EnrollStudentAsync(EnrollmentDTO enrollmentDto);
        Task UnenrollStudentAsync(int studentId, int courseId);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
    }
}
