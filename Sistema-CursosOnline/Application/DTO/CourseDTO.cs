namespace Sistema_CursosOnline.Application.DTO
{
    public record CourseDTO(int Id, string Title, string Description, int InstructorId, string Status, DateTime CreationDate);
}
