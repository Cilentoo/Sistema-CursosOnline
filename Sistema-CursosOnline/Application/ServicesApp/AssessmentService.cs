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

        public async Task<IEnumerable<AssessmentDTO>> GetByCourseIdAsync(int courseId)
        {
            var assessments = await _assessmentRepository.GetByCourseIdAsync(courseId);
            return assessments.Select(a => AssessmentDTO.FromEntity(a));
        }

        public async Task<IEnumerable<AssessmentDTO>> GetByUserIdAsync(int userId)
        {
            var assessments = await _assessmentRepository.GetByUserIdAsync(userId);
            return assessments.Select(a => AssessmentDTO.FromEntity(a));
        }

        public async Task AddAsync(AssessmentDTO assessmentDto)
        {
            var assessment = new Assessment(assessmentDto.Id, assessmentDto.CourseId, assessmentDto.UserId, assessmentDto.Notes, assessmentDto.Comment, assessmentDto.CreationDate);
            await _assessmentRepository.AddAsync(assessment);
        }

        public async Task UpdateAsync(AssessmentDTO assessmentDto)
        {
            var assessment = new Assessment(assessmentDto.Id, assessmentDto.CourseId, assessmentDto.UserId, assessmentDto.Notes, assessmentDto.Comment, assessmentDto.CreationDate);
            await _assessmentRepository.UpdateAsync(assessment);
        }

        public async Task DeleteAsync(int id)
        {
            await _assessmentRepository.DeleteAsync(id);
        }

    }
}
