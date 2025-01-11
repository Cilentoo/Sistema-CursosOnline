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
        private const string ImgurClientId = "1f23293e27378e5";

        public CourseService(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<IEnumerable<CourseDTO>> GetAllAsync()
        {
            var courses = await _courseRepository.GetAllAsync();
            return MapToDtoList(courses);
        }

        public async Task<CourseDTO> GetByIdAsync(int id)
        {
            var course = await _courseRepository.GetByIdAsync(id);
            return MapToDto(course);
        }

        public async Task<IEnumerable<CourseDTO>> GetByStatusAsync(string status)
        {
            var courses = await _courseRepository.GetByStatusAsync(status);
            return MapToDtoList(courses);
        }

        public async Task AddAsync(CourseDTO courseDTO)
        {
            var photoUrl = await UploadImageToImgur(courseDTO.PhotoURL);
            courseDTO.PhotoURL = photoUrl;
            var course = MapToEntity(courseDTO);
            await _courseRepository.AddAsync(course);
        }

        public async Task UpdateAsync(CourseDTO courseDTO)
        {
            if (!string.IsNullOrEmpty(courseDTO.PhotoURL))
            {
                var photoUrl = await UploadImageToImgur(courseDTO.PhotoURL);
                courseDTO.PhotoURL = photoUrl;
            }

            var course = MapToEntity(courseDTO);
            await _courseRepository.UpdateAsync(course);
        }

        public async Task InactiveCourseAsync(int id)
        {
            await _courseRepository.InactiveCourseAsync(id);
        }

        private async Task<string> UploadImageToImgur(string base64Image)
        {
            var client = new RestClient("https://api.imgur.com/3/image");
            var request = new RestRequest("POST");
            request.AddHeader("Authorization", $"Client-ID {ImgurClientId}");
            request.AddParameter("image", base64Image);

            var response = await client.ExecuteAsync<ImgurResponse>(request);
            if (response.IsSuccessful && response.Data != null && response.Data.Data != null)
            {
                return response.Data.Data.Link; 
            }
            throw new Exception("Erro ao fazer upload da imagem no Imgur: " + response.ErrorMessage);
        }

        private static CourseDTO MapToDto(Course course)
        {
            return new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                InstructorName = course.InstructorId?.Name,
                CreationDate = course.CreationDate,
                Status = course.Status,
                PhotoURL = course.PhotoURL,
            };
        }

        private static IEnumerable<CourseDTO> MapToDtoList(IEnumerable<Course> courses)
        {
            return courses.Select(course => new CourseDTO
            {
                Id = course.Id,
                Title = course.Title,
                Description = course.Description,
                InstructorName = course.InstructorId?.Name,
                CreationDate = course.CreationDate,
                Status = course.Status,
                PhotoURL = course.PhotoURL
            });
        }

        private static Course MapToEntity(CourseDTO courseDTO)
        {
            return new Course
            {
                Id = courseDTO.Id,
                Title = courseDTO.Title,
                Description = courseDTO.Description,
                InstructorId = new User { Name = courseDTO.InstructorName },
                CreationDate = courseDTO.CreationDate,
                Status = courseDTO.Status,
                PhotoURL = courseDTO.PhotoURL
            };
        }
    }
}
