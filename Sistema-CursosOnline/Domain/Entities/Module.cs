namespace Sistema_CursosOnline.Domain.Entities
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Course CourseId { get; set; }


        public Module(int id, string name, string description, Course courseId)
        {
            Id = id;
            Name = name;
            Description = description;
            CourseId = courseId;
        }

        public Module() { }
    }
}
