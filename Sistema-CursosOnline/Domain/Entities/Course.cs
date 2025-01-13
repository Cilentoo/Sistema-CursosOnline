
namespace Sistema_CursosOnline.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] CoverImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
        public List<Module> Modules { get; set; }
        public List<Assessments> Assessments { get; set; }

        public Course()
        {
        }

        public Course(int id, string title, string description, byte[] coverImage, DateTime createdAt, int instructorId, User instructor)
        {
            Id = id;
            Title = title;
            Description = description;
            CoverImage = coverImage;
            CreatedAt = createdAt;
            InstructorId = instructorId;
            Instructor = instructor;
            Modules = new List<Module>();
            Assessments = new List<Assessment>();
        }
    }
}
