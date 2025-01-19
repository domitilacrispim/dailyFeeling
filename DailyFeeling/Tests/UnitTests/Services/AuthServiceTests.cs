using DailyFeeling.DTOs;
using DailyFeeling.Models;
using DailyFeeling.Repositories;
using DailyFeeling.Services;
using Moq;

namespace UnitTests.Services;

public class AuthServiceTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly AuthService _authService;

        public AuthServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _authService = new AuthService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task RegisterAsync_EmailAlreadyExists_ReturnsEmailAlreadyExistsMessage()
        {
            // Arrange
            var userRequest = new RegisterRequest { Email = "test@example.com", Username = "testuser", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.EmailExistsAsync("test@example.com")).ReturnsAsync(true);

            // Act
            var result = await _authService.RegisterAsync(userRequest);

            // Assert
            Assert.Equal("Email já cadastrado.", result);
        }

        [Fact]
        public async Task RegisterAsync_UsernameAlreadyExists_ReturnsUsernameAlreadyExistsMessage()
        {
            // Arrange
            var userRequest = new RegisterRequest { Email = "newemail@example.com", Username = "testuser", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.EmailExistsAsync("newemail@example.com")).ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.UsernameExistsAsync("testuser")).ReturnsAsync(true);

            // Act
            var result = await _authService.RegisterAsync(userRequest);

            // Assert
            Assert.Equal("Username já cadastrado", result);
        }

        [Fact]
        public async Task RegisterAsync_SuccessfullyRegistersUser_ReturnsNull()
        {
            // Arrange
            var userRequest = new RegisterRequest { Email = "newemail@example.com", Username = "newuser", Password = "password" };
            _userRepositoryMock.Setup(repo => repo.EmailExistsAsync("newemail@example.com")).ReturnsAsync(false);
            _userRepositoryMock.Setup(repo => repo.UsernameExistsAsync("newuser")).ReturnsAsync(false);

            // Act
            var result = await _authService.RegisterAsync(userRequest);

            // Assert
            Assert.Null(result);
            _userRepositoryMock.Verify(repo => repo.AddUserAsync(It.IsAny<User>()), Times.Once);
        }
    }