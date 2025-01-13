namespace Sistema_CursosOnline.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; } 

        public Course Course { get; set; } 
        public User Student { get; set; }
    }
}
