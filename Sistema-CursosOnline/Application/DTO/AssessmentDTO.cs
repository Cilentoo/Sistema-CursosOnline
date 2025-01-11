using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.DTO
{
    public class AssessmentDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int UserId { get; set; }
        public double Notes { get; set; }
        public string Comment { get; set; }
        public DateTime CreationDate { get; set; }

        public AssessmentDTO(int id, int courseId, int userId, double notes, string comment, DateTime creationDate)
        {
            Id = id;
            CourseId = courseId;
            UserId = userId;
            Notes = notes;
            Comment = comment;
            CreationDate = creationDate;
        }

        public static AssessmentDTO FromEntity(Assessment assessment)
        {
            return new AssessmentDTO(assessment.Id, assessment.CourseId, assessment.UserId, assessment.Notes, assessment.Comment, assessment.CreationDate);
        }
    }
}
