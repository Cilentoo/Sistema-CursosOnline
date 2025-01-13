using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.tests.Tests
{
    public class AssessmentServiceTest
    {
        private readonly Mock<IAssessmentRepository> _mockAssessmentRepository;
        private readonly AssessmentService _assessmentService;

        public AssessmentServiceTest()
        {
            _mockAssessmentRepository = new Mock<IAssessmentRepository>();
            _assessmentService = new AssessmentService(_mockAssessmentRepository.Object);
        }

        [Fact]
        public async Task CreateAssessmentAsync_ShouldAddAssessment_WhenValidDataIsProvided()
        {
            var assessmentDto = new AssessmentDTO
            {
                CourseId = 1,
                StudentId = 2,
                Rating = 4,
                Comment = "Great course!"
            };

            _mockAssessmentRepository.Setup(repo => repo.CreateAsync(It.IsAny<Assessment>()))
                .Returns(Task.CompletedTask);  

            await _assessmentService.CreateAssessmentAsync(assessmentDto);

            _mockAssessmentRepository.Verify(repo => repo.CreateAsync(It.IsAny<Assessment>()), Times.Once);
        }
    }
}
