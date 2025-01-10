namespace Sistema_CursosOnline.Application.DTO
{
   public record AssessmentDTO(int Id, int CourseId, int UsuarioId, double Notes, string Comment, DateTime CreationDate );
}
