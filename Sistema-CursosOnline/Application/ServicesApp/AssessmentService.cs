using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Domain.Entities;

namespace Sistema_CursosOnline.Application.ServicesApp
{
    public class AssessmentService
    {
        private readonly IAssessmentRepository _assessmentRepository;

        public AssessmentService(IAssessmentRepository AssessmentRepository)
        {
            _assessmentRepository = _assessmentRepository;
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

        public async Task UpdateAssessmentAsync(int id, AssessmentDTO assessmentDto)
        {
            var assessment = await _assessmentRepository.GetByCourseIdAsync(id);
            if (assessment != null)
            {
                assessment.Rating = assessment.Rating;
                assessment.Comment = assessment.Comment;

                await _assessmentRepository.UpdateAsync(assessment);
            }
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
