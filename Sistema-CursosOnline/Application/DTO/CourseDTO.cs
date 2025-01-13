using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public byte[] PhotoData { get; set; }
    }
}
