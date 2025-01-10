
namespace Sistema_CursosOnline.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public User InstructorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }

        public Course (int id, string title, string description, User instructorId, DateTime creationDate, string status)
        {
            Id = id;
            Title = title;
            Description = description;
            InstructorId = instructorId;
            CreationDate = creationDate;
            Status = status;
        }

        public Course() {}
    }
}
