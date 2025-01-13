namespace Sistema_CursosOnline.Domain.Entities
{
    public class Assessment
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public int Rating { get; set; }  
        public string Comment { get; set; }

        public Course Course { get; set; }  
        public User Student { get; set; }
    }
}
