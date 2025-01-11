namespace Sistema_CursosOnline.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }

        public Enrollment(int studentId, int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
            EnrollmentDate = DateTime.Now;
        }

        public Enrollment() { }
    }
}
