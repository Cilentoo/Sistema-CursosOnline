namespace Sistema_CursosOnline.Domain.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public double Notes { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }

        public Assessment(int id, int courseId, int userId, double notes, string comment, DateTime creationDate)
        {
            Id = id;
            CourseId = courseId;
            UserId = userId;
            Notes = notes;
            Comment = comment;
            CreationDate = creationDate;
        }

        public Assessment() {}
    }
}
