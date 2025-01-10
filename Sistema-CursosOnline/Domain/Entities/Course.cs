
namespace Sistema_CursosOnline.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }

        public Course (int id, string title, string description, int instructorId, User instructor, DateTime creationDate, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            InstructorId = instructorId;
            Instructor = instructor;
            CreationDate = creationDate;
            Status = status;
        }

        public Course() {}
    }
}
