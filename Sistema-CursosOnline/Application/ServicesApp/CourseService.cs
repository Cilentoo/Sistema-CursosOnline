using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.Messaging;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IUserRepository _userRepository;
        private readonly RabbitMqConfig _rabbitMqConfiguration;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
            _rabbitMqConfiguration = new RabbitMqConfig();
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
                CoverImage = course.CoverImage != null ? Convert.ToBase64String(course.CoverImage) : null,
                CreatedAt = course.CreatedAt,
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
                CoverImage = course.CoverImage != null ? Convert.ToBase64String(course.CoverImage) : null,
                CreatedAt = course.CreatedAt,
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
                CoverImage = course.CoverImage != null ? Convert.ToBase64String(course.CoverImage) : null,
                CreatedAt = course.CreatedAt,
                InstructorId = course.Instructor.Id
            });
        }

        public async Task AddCourseAsync(CourseDTO courseDto)
        {

            var instructor = await _userRepository.GetByIdAsync(courseDto.InstructorId);
            if (instructor == null) throw new Exception("Instructor not found");


            byte[] coverImageBytes = null;

            if (!string.IsNullOrEmpty(courseDto.CoverImage))
            {
              
                if (courseDto.CoverImage.Contains(","))
                {
                    coverImageBytes = Convert.FromBase64String(courseDto.CoverImage.Split(',')[1]);
                }
                else
                {
                    coverImageBytes = Convert.FromBase64String(courseDto.CoverImage);  
                }
            }


            var course = new Course
            {
                Title = courseDto.Title,
                Description = courseDto.Description,
                CoverImage = coverImageBytes,
                CreatedAt = DateTime.UtcNow,
                InstructorId = courseDto.InstructorId,
                Instructor = instructor,
            };

            await _courseRepository.AddAsync(course);

            using (var connection = _rabbitMqConfiguration.CreateConnection())
            using (var channel = _rabbitMqConfiguration.CreateChannel(connection))
            {
                var message = $"Curso {courseDto.Title} foi criado";
                _rabbitMqConfiguration.SendMessage(channel, message);
            }
        }

        public async Task UpdateCourseAsync(CourseDTO courseDto)
        {
            if (courseDto == null)
                throw new ArgumentNullException(nameof(courseDto), "O objeto de curso não pode ser nulo.");

            var existingCourse = await _courseRepository.GetByIdAsync(courseDto.Id);
            if (existingCourse == null)
                throw new Exception($"Curso com ID {courseDto.Id} não encontrado.");

            var instructor = await _userRepository.GetByIdAsync(courseDto.InstructorId);
            if (instructor == null || instructor.Role != EType.Instructor)
                throw new Exception("Instrutor não encontrado ou o usuário não tem o papel de instrutor.");

            byte[] coverImageBytes = null;

            if (!string.IsNullOrEmpty(courseDto.CoverImage))
            {
                if (courseDto.CoverImage.Contains(","))
                {
                    coverImageBytes = Convert.FromBase64String(courseDto.CoverImage.Split(',')[1]);
                }
                else
                {
                    coverImageBytes = Convert.FromBase64String(courseDto.CoverImage);
                }
            }

            existingCourse.Title = courseDto.Title;
            existingCourse.Description = courseDto.Description;
            existingCourse.CoverImage = coverImageBytes ?? existingCourse.CoverImage;
            existingCourse.InstructorId = courseDto.InstructorId;
            existingCourse.Instructor = instructor;

            await _courseRepository.UpdateAsync(existingCourse);
        }

        public async Task DeleteCourseAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            if (course == null) throw new Exception("Curso não encontrado");

            await _courseRepository.DeleteAsync(id);

            using (var connection = _rabbitMqConfiguration.CreateConnection())
            using (var channel = _rabbitMqConfiguration.CreateChannel(connection))
            {
                var message = $"Curso {course.Title} foi deletado";
                _rabbitMqConfiguration.SendMessage(channel, message);
            }
        }
    }
}
