using Moq;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.tests.Tests
{
    public class EnrollmentServiceTest
    {
        private readonly Mock<IEnrollmentRepository> _mockEnrollmentRepository;
        private readonly EnrollmentService _enrollmentService;

        public EnrollmentServiceTest()
        {
            _mockEnrollmentRepository = new Mock<IEnrollmentRepository>();
            _enrollmentService = new EnrollmentService(_mockEnrollmentRepository.Object);
        }

        [Fact]
        public async Task EnrollStudentAsync_ShouldEnroll_WhenStudentIsNotAlreadyEnrolled()
        {

            var enrollmentDto = new EnrollmentDTO
            {
                StudentId = 1,
                CourseId = 101,
                EnrollmentDate = DateTime.UtcNow
            };

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(enrollmentDto.StudentId, enrollmentDto.CourseId))
                .ReturnsAsync((Enrollment)null); 

            _mockEnrollmentRepository.Setup(repo => repo.CreateAsync(It.IsAny<Enrollment>()))
                .Returns(Task.CompletedTask);


            await _enrollmentService.EnrollStudentAsync(enrollmentDto);


            _mockEnrollmentRepository.Verify(repo => repo.CreateAsync(It.IsAny<Enrollment>()), Times.Once);
        }

        [Fact]
        public async Task EnrollStudentAsync_ShouldThrowException_WhenStudentIsAlreadyEnrolled()
        {

            var enrollmentDto = new EnrollmentDTO
            {
                StudentId = 1,
                CourseId = 101,
                EnrollmentDate = DateTime.UtcNow
            };

            var existingEnrollment = new Enrollment
            {
                StudentId = 1,
                CourseId = 101,
                EnrollmentDate = DateTime.UtcNow
            };

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(enrollmentDto.StudentId, enrollmentDto.CourseId))
                .ReturnsAsync(existingEnrollment); 


            var exception = await Assert.ThrowsAsync<Exception>(() => _enrollmentService.EnrollStudentAsync(enrollmentDto));
            Assert.Equal("O aluno já está inscrito neste curso.", exception.Message);
        }

        [Fact]
        public async Task UnenrollStudentAsync_ShouldUnenroll_WhenStudentIsEnrolled()
        {
            var studentId = 1;
            var courseId = 101;

            var existingEnrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow
            };

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(studentId, courseId))
                .ReturnsAsync(existingEnrollment);

            _mockEnrollmentRepository.Setup(repo => repo.RemoveAsync(It.IsAny<int>()))
                .Returns(Task.CompletedTask);


            await _enrollmentService.UnenrollStudentAsync(studentId, courseId);


            _mockEnrollmentRepository.Verify(repo => repo.RemoveAsync(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task UnenrollStudentAsync_ShouldThrowException_WhenStudentIsNotEnrolled()
        {

            var studentId = 1;
            var courseId = 101;

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(studentId, courseId))
                .ReturnsAsync((Enrollment)null); 


            var exception = await Assert.ThrowsAsync<Exception>(() => _enrollmentService.UnenrollStudentAsync(studentId, courseId));
            Assert.Equal("O aluno não está inscrito neste curso.", exception.Message);
        }

        [Fact]
        public async Task IsEnrolledAsync_ShouldReturnTrue_WhenStudentIsEnrolled()
        {

            var studentId = 1;
            var courseId = 101;

            var existingEnrollment = new Enrollment
            {
                StudentId = studentId,
                CourseId = courseId,
                EnrollmentDate = DateTime.UtcNow
            };

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(studentId, courseId))
                .ReturnsAsync(existingEnrollment); 


            var result = await _enrollmentService.IsEnrolledAsync(studentId, courseId);

            Assert.True(result);
        }

        [Fact]
        public async Task IsEnrolledAsync_ShouldReturnFalse_WhenStudentIsNotEnrolled()
        {

            var studentId = 1;
            var courseId = 101;

            _mockEnrollmentRepository.Setup(repo => repo.GetByStudentAndCourseIdAsync(studentId, courseId))
                .ReturnsAsync((Enrollment)null);


            var result = await _enrollmentService.IsEnrolledAsync(studentId, courseId);


            Assert.False(result);
        }
    }
}
