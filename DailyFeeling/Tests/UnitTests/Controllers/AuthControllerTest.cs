using DailyFeeling.Controllers;
using DailyFeeling.DTOs;
using DailyFeeling.Models;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;

namespace UnitTests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        
        var configurationMock = new Mock<IConfiguration>();
        configurationMock.Setup(c => c["Jwt:SecretKey"]).Returns("a34530c30dec412cdvwfvnsdfosdfigrea353de1d1sdvfkvlsd332c70dd8a54c0b54a");
        configurationMock.Setup(c => c["Jwt:TokenExpirationInHours"]).Returns("24");

        var jwtService = new JwtService(configurationMock.Object);
        _authController = new AuthController(_authServiceMock.Object, jwtService);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterRequest { Username = "test", Email = "test@example.com", Password = "password" };
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
            .ReturnsAsync(ApiResponse<User?>.ErrorResponse(409, "Email already exists"));

        // Act
        var result = await _authController.Register(request);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(409, actionResult.StatusCode);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenUserIsRegistered()
    {
        // Arrange
        var request = new RegisterRequest { Username = "test", Email = "test@example.com", Password = "password" };
        var user = new User { CreatedAt = DateTime.Now, Email = request.Email, Username = request.Username, Id = 0, PasswordHash = "abcd" };
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
            .ReturnsAsync(ApiResponse<User?>.SuccessResponse(user));  // Nenhum erro, usuário registrado com sucesso.

        // Act
        var result = await _authController.Register(request);

        // Assert
        var actionResult = Assert.IsType<ObjectResult>(result);
        Assert.Equal(200, actionResult.StatusCode);
    }
}