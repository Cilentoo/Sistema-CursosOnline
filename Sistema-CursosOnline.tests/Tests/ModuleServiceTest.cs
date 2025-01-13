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
    public class ModuleServiceTest
    {
        private readonly Mock<IModuleRepository> _mockModuleRepository;
        private readonly ModuleService _moduleService;

        public ModuleServiceTest()
        {
            _mockModuleRepository = new Mock<IModuleRepository>();
            _moduleService = new ModuleService(_mockModuleRepository.Object);
        }

        [Fact]
        public async Task CreateModuleAsync_ShouldAddModule_WhenValidDataIsProvided()
        {
            var moduleDto = new ModuleDTO
            {
                Title = "New Module",
                CourseId = 1,
                Description = "Module Description"
            };

            _mockModuleRepository.Setup(repo => repo.CreateAsync(It.IsAny<Module>()))
                .Returns(Task.CompletedTask);

            await _moduleService.CreateModuleAsync(moduleDto);

            _mockModuleRepository.Verify(repo => repo.CreateAsync(It.IsAny<Module>()), Times.Once);
        }

        [Fact]
        public async Task UpdateModuleAsync_ShouldUpdateModule_WhenModuleExists()
        {
            var moduleId = 1;
            var moduleDto = new ModuleDTO
            {
                Title = "Updated Module",
                Description = "Updated Description"
            };
            var module = new Module
            {
                Id = moduleId,
                Title = "Old Module",
                Description = "Old Description"
            };

            _mockModuleRepository.Setup(repo => repo.GetByIdAsync(moduleId))
                .ReturnsAsync(module);

            _mockModuleRepository.Setup(repo => repo.UpdateAsync(It.IsAny<Module>()))
                .Returns(Task.CompletedTask);

            await _moduleService.UpdateModuleAsync(moduleId, moduleDto);

            Assert.Equal(moduleDto.Title, module.Title);
            Assert.Equal(moduleDto.Description, module.Description);
            _mockModuleRepository.Verify(repo => repo.UpdateAsync(It.IsAny<Module>()), Times.Once);
        }
    }
}
