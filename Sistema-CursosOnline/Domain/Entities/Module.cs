namespace Sistema_CursosOnline.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int LessonCount { get; set; }

        public Course Course { get; set; }
    }
}
