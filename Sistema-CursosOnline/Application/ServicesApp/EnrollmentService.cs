using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
        public class EnrollmentService : IEnrollmentService
        {
            private readonly IEnrollmentRepository _enrollmentRepository;
            private readonly ICourseRepository _courseRepository;

            public EnrollmentService(IEnrollmentRepository enrollmentRepository, ICourseRepository courseRepository)
            {
                _enrollmentRepository = enrollmentRepository;
                _courseRepository = courseRepository;
            }

            public async Task EnrollAsync(EnrollmentDTO enrollmentDto)
            {
                var course = await _courseRepository.GetByIdAsync(enrollmentDto.CourseId);

                if (course.Status != "Ativo")
                {
                    throw new InvalidOperationException("Curso não está ativo.");
                }

                var existingEnrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
                if (existingEnrollment != null)
                {
                    throw new InvalidOperationException("Aluno já está inscrito neste curso.");
                }

                var enrollment = new Enrollment(enrollmentDto.StudentId, enrollmentDto.CourseId);
                await _enrollmentRepository.AddEnrollmentAsync(enrollment);
            }

        public async Task UnenrollAsync(EnrollmentDTO enrollmentDto)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
            if (enrollment == null)
            {
                throw new InvalidOperationException("Aluno não está inscrito neste curso.");
            }

            await _enrollmentRepository.RemoveEnrollmentAsync(enrollment.Id);
        }

        public async Task<bool> IsEnrolledAsync(EnrollmentDTO enrollmentDto)
        {
            var enrollment = await _enrollmentRepository.GetEnrollmentAsync(enrollmentDto.StudentId, enrollmentDto.CourseId);
            return enrollment != null;
        }

    }
}