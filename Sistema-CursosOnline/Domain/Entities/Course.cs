
namespace Sistema_CursosOnline.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public byte[] PhotoData { get; set; }

        public Course (int id, string title, string description, int instructorId, DateTime creationDate, string status, byte[] photoData)
        {
            Id = id;
            Title = title;
            Description = description;
            InstructorId = instructorId;
            CreationDate = creationDate;
            Status = status;
            PhotoData = photoData;
        }

        public Course() {}
    }
}
