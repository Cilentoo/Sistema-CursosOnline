using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.tests.Tests
{
    public class CourseServiceTest
    {
        private readonly Mock<ICourseRepository> _mockCourseRepository;
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly CourseService _courseService;


        public CourseServiceTest()
        {
                  _mockCourseRepository = new Mock<ICourseRepository>();
            _mockUserRepository = new Mock<IUserRepository>();
            _courseService = new CourseService(_mockCourseRepository.Object, _mockUserRepository.Object);
        }

        [Fact]
        public async Task CreateCourseAsync_ShouldCreateCourse_WhenValidDataIsProvided()
        {
            var courseDto = new CourseDTO
            {
                Title = "New Course",
                Description = "Course Description",
                InstructorId = 1
            };
            _mockUserRepository.Setup(repo => repo.GetByIdAsync(courseDto.InstructorId))
                .ReturnsAsync(new User { Id = 1, Name = "Instructor Name", Role = EType.Instructor });

            _mockCourseRepository.Setup(repo => repo.AddAsync(It.IsAny<Course>()))
                .Returns(Task.CompletedTask);

            await _courseService.AddCourseAsync(courseDto);

            
            _mockCourseRepository.Verify(repo => repo.AddAsync(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task GetCoursesByInstructorIdAsync_ShouldReturnCourses_WhenInstructorExists()
        {
            var instructorId = 1;
            var courses = new List<Course>
            {
                new Course { Id = 1, Title = "Course 1", InstructorId = instructorId },
                new Course { Id = 2, Title = "Course 2", InstructorId = instructorId }
            };

            _mockCourseRepository.Setup(repo => repo.GetByInstructorIdAsync(instructorId))
                .ReturnsAsync(courses);


            var result = await _courseService.GetCoursesByInstructorIdAsync(instructorId);

            Assert.NotNull(result);
            _mockCourseRepository.Verify(repo => repo.GetByInstructorIdAsync(instructorId), Times.Once);
        }
    
    }
}
