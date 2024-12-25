using DailyFeeling.Controllers;
using DailyFeeling.DTOs;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTests.Controllers;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _authServiceMock;
    private readonly AuthController _authController;

    public AuthControllerTests()
    {
        _authServiceMock = new Mock<IAuthService>();
        _authController = new AuthController(_authServiceMock.Object);
    }

    [Fact]
    public async Task Register_ReturnsBadRequest_WhenEmailAlreadyExists()
    {
        // Arrange
        var request = new RegisterRequest { Username = "test", Email = "test@example.com", Password = "password" };
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
            .ReturnsAsync("Email já cadastrado.");

        // Act
        var result = await _authController.Register(request);

        // Assert
        var actionResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Email já cadastrado.", actionResult.Value);
    }

    [Fact]
    public async Task Register_ReturnsOk_WhenUserIsRegistered()
    {
        // Arrange
        var request = new RegisterRequest { Username = "test", Email = "test@example.com", Password = "password" };
        _authServiceMock.Setup(s => s.RegisterAsync(It.IsAny<RegisterRequest>()))
            .ReturnsAsync((string?)null);  // Nenhum erro, usuário registrado com sucesso.

        // Act
        var result = await _authController.Register(request);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal("Usuário registrado com sucesso.", actionResult.Value);
    }
}