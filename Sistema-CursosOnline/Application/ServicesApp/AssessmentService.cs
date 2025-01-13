using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;
using Sistema_CursosOnline.Domain.IServices;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class AssessmentService : IAssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;

        public AssessmentService(IAssessmentRepository assessmentRepository)
        {
            _assessmentRepository = assessmentRepository;
        }

        public async Task CreateAssessmentAsync(AssessmentDTO assessmentDto)
        {
            var assessment = new Assessment
            {
                CourseId = assessmentDto.CourseId,
                StudentId = assessmentDto.StudentId,
                Rating = assessmentDto.Rating,
                Comment = assessmentDto.Comment
            };

            await _assessmentRepository.CreateAsync(assessment);
        }

        public async Task DeleteAssessmentAsync(int id)
        {
            await _assessmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Assessment>> GetAssessmentByCourseIdAsync(int courseId)
        {
            return await _assessmentRepository.GetByCourseIdAsync(courseId);
        }
    }
}
