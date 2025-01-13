namespace Sistema_CursosOnline.Application.DTO
{
    public class EnrollmentDTO
    {
        public int StudentId { get; set; }    // Representa o aluno que se inscreve
        public int CourseId { get; set; }     // Representa o curso no qual o aluno está se inscrevendo
        public DateTime EnrollmentDate { get; set; }
    }
}
