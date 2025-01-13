using Sistema_CursosOnline.Application.DTO;

namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IEnrollmentService
    {
        Task EnrollAsync(EnrollmentDTO enrollmentDTO);
        Task UnenrollAsync(EnrollmentDTO enrollmentDTO);
        Task<bool> IsEnrolledAsync(EnrollmentDTO enrollmentDTO);
    }
}
