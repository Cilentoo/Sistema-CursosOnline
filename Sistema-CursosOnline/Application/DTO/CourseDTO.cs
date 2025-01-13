namespace Sistema_CursosOnline.Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string CoverImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public int InstructorId { get; set; }
    }
}
