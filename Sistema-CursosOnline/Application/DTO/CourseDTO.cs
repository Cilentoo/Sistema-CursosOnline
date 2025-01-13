namespace Sistema_CursosOnline.Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public byte[] CoverImage { get; set; }
        public DateTime CreatedAt { get; set; }
        public string InstructorName { get; set; }
        public int InstructorId { get; set; }
    }
}
