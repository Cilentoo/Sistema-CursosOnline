namespace Sistema_CursosOnline.Domain.Entities
{
    public class Course
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Description { get; set; }
        public int InstructorId { get; set; }
        public User Instructor { get; set; }
        public DateTime CreationDate { get; set; }
        public string Status { get; set; }

        public Course (int id, string titulo, string description, int instructorId, User instructor, DateTime creationDate, string status)
        {
            Id = id;
            Titulo = titulo;
            Description = description;
            InstructorId = instructorId;
            Instructor = instructor;
            CreationDate = creationDate;
            Status = status;
        }

        public Course() {}

        public bool ValidarCurso()
        {
            if (string.IsNullOrEmpty(Titulo) || Titulo.Length < 3)
            {
                return false;
            }
            return true;
        }
    }
}
