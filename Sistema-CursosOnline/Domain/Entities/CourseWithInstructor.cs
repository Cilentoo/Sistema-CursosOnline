namespace Sistema_CursosOnline.Domain.Entities
{
    public class CourseWithInstructor : Course
    {
        public int InstructorId { get; set; }
        public string InstructorName { get; set; }
    }
}
