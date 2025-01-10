using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.DTO
{
    public record CourseDTO(int Id, string Title, string Description, User InstructorId, string Status, DateTime CreationDate);
}
