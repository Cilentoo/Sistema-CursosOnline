using Moq;
using Sistema_CursosOnline.Application.DTO;
using Sistema_CursosOnline.Application.ServicesApp;
using Sistema_CursosOnline.Domain.Entities;
using Sistema_CursosOnline.Domain.IRepository;

namespace Sistema_CursosOnline.tests.Tests
{
    public class UserServiceTest
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly UserService _userService;

        public UserServiceTest()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object, "TestSecretKey", "30");
        }

        [Fact]
        public async Task AuthenticateAsync_ShouldReturnToken_WhenAreCorrect()
        {
           
            var email = "test@example.com";
            var password = "password";

            _userRepositoryMock.Setup(repo => repo.AuthenticateAsync(email, password))
                .ReturnsAsync(new User
                {
                    Id = 1,
                    Name = "Test User",
                    Role = EType.Student,
                    Status = "Ativo"
                });

            var userService = new UserService(_userRepositoryMock.Object, "E4f0RXDhAe7k7mFk1LZ3NtP7sX+BkCJZ9oPU8RuyD9rKZRPLWtDZWm2vQhFJHjzn", "60");

            var result = await userService.AuthenticateAsync(email, password);

            Assert.NotNull(result);
        }

        [Fact]
        public async Task RegisterAsync_ShouldRegisterUser()
        {
            var userDto = new UserDTO
            {
                Name = "Jonh Doe",
                Email = "jonh.doe@test.com",
                Role = "Student"
            };

            _userRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<string>())).Returns(Task.CompletedTask);

            var result = await _userService.RegisterAsync(userDto, "password");
            Assert.NotNull(result);
            Assert.Equal(userDto.Email, result.Email);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task InactivateUserAsync_ShouldInactivateUser_WhenUserExists()
        {
    
            var userId = 1;
            var user = new User { Id = userId, Status = "Inativo" };

            _userRepositoryMock.Setup(repo => repo.GetByIdAsync(userId))
                .ReturnsAsync(user);

            _userRepositoryMock.Setup(repo => repo.InactiveAsync(userId))
                .Returns(Task.CompletedTask);

            await _userService.InactivateUserAsync(userId);

            Assert.Equal("Inativo", user.Status);
            _userRepositoryMock.Verify(repo => repo.InactiveAsync(userId), Times.Once);
        }
    }
}
