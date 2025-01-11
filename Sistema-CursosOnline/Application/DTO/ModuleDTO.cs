using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.DTO
{
    public class ModuleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int CourseId { get; set; }

        public ModuleDTO(int id, string name, string description, int courseId)
        {
            Id = id;
            Name = name;
            Description = description;
            CourseId = courseId;
        }

        public static ModuleDTO FromEntity(Module module)
        {
            return new ModuleDTO(module.Id, module.Name, module.Description, module.CourseId.Id); 
        }

        public Module ToEntity()
        {
            return new Module(Id, Name, Description, new Course { Id = CourseId });
        }
    }
}
