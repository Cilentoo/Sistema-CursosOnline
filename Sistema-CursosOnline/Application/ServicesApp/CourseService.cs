using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class CourseService : ICourseService
    {
        private readonly ICourseRepository _courseRepository;

        public CourseService(ICourseRepository courseRepository) 
        {
            _courseRepository = courseRepository;
        }

        public async Task AddAsync(CourseDTO courseDTO)
        {
            var course = new Course
            {
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                CreationDate = courseDTO.CreationDate,
                InstructorId = courseDTO.InstructorId,
                Status = courseDTO.Status,
            };

            await _courseRepository.AddAsync(course);

            //RABBITMQ
        }
    }
}
