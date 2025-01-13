using RestSharp;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.Response;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return MapToDtoList(courses);
        }

        public async Task<CourseDTO> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);

            var instructor = await _userRepository.GetByIdAsync(course.InstructorId);
            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreationDate = course.CreationDate,
                Status = course.Status,
                PhotoData = course.PhotoData
            };
        }

        public async Task<IEnumerable<CourseDTO>> GetByStatusAsync(string status)
        {
            var courses = await _courseRepository.GetByStatusAsync(status);
            return MapToDtoList(courses);
        }

        public async Task AddAsync(CourseDTO courseDTO, int instructorId)
        {
            var course = new Course
            {
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                InstructorId = instructorId,
                CreationDate = DateTime.UtcNow,
                Status = courseDTO.Status,
                PhotoData = courseDTO.PhotoData
            };

            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateAsync(CourseDTO courseDTO)
        {
            var existingCourse = await _courseRepository.GetByIdAsync(courseDTO.Id);
            if (existingCourse == null)
            {
                throw new InvalidOperationException("Curso não encontrado.");
            }

            existingCourse.Title = courseDTO.Title;
            existingCourse.Description = courseDTO.Description;
            existingCourse.Status = courseDTO.Status;
            existingCourse.PhotoData = courseDTO.PhotoData;

            await _courseRepository.UpdateAsync(existingCourse);
        }

        public async Task InactiveCourseAsync(int id)
        {
            await _courseRepository.InactiveCourseAsync(id);
        }


        private static CourseDTO MapToDto(Course course)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreationDate = course.CreationDate,
                Status = course.Status,
                PhotoData = course.PhotoData,
            };
        }

        private static IEnumerable<CourseDTO> MapToDtoList(IEnumerable<Course> courses)
        {
            return courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CreationDate = course.CreationDate,
                Status = course.Status,
                PhotoData = course.PhotoData
            });
        }

        private static Course MapToEntity(CourseDTO courseDTO, int instructorId)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                InstructorId = instructorId,
                CreationDate = courseDTO.CreationDate,
                Status = courseDTO.Status,
                PhotoData = courseDTO.PhotoData
            };
        }
    }
}
