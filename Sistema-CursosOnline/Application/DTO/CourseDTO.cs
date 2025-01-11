namespace Sistema_CursosOnline.Application.DTO
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string InstructorName { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }
        public string PhotoURL { get; set; }
    }
}
