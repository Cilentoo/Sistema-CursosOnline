namespace Sistema_CursosOnline.Domain.IServices
{
    public interface IEnrollmentService
    {
        Task EnrollAsync(int studentId, int courseId);
        Task UnenrollAsync(int studentId, int courseId);
        Task<bool> IsEnrolledAsync(int studentId, int courseId);
    }
}
