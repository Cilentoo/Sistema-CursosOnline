using Sistema_CursosOnline.Application.DTO;
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

        public async Task<CourseDTO> GetCourseByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) return null;

            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CoverImage = course.CoverImage,
                CreatedAt = course.CreatedAt,
                InstructorName = course.Instructor.Name,
                InstructorId = course.Instructor.Id  
            };
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByInstructorIdAsync(int instructorId)
        {
            var courses = await _courseRepository.GetByInstructorIdAsync(instructorId);

            return courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CoverImage = course.CoverImage,
                CreatedAt = course.CreatedAt,
                InstructorName = course.Instructor.Name,  
                InstructorId = course.Instructor.Id
            });
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCoursesAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                CoverImage = course.CoverImage,
                CreatedAt = course.CreatedAt,
                InstructorName = course.Instructor.Name,
                InstructorId = course.Instructor.Id
            });
        }

        public async Task AddCourseAsync(CourseDTO courseDto)
        {
            
            var instructor = await _userRepository.GetByIdAsync(courseDto.InstructorId);
            if (instructor == null) throw new Exception("Instructor not found");

            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                CoverImage = courseDto.CoverImage,
                CreatedAt = DateTime.UtcNow,
                Instructor = instructor
            };

            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateCourseAsync(CourseDTO courseDto)
        {
      
            var instructor = await _userRepository.GetByIdAsync(courseDto.InstructorId);
            if (instructor == null) throw new Exception("Instructor not found");

          
            var course = new Course
            {
                Id = courseDto.Id,
                Title = courseDto.Title,
                Description = courseDto.Description,
                CoverImage = courseDto.CoverImage,
                CreatedAt = courseDto.CreatedAt,
                Instructor = instructor
            };

            await _courseRepository.UpdateAsync(course);
        }

        public async Task DeleteCourseAsync(int id)
        {
            await _courseRepository.DeleteAsync(id);
        }
    }
}
