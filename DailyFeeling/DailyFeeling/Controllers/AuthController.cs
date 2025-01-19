using DailyFeeling.DTOs;
using DailyFeeling.Models;
using DailyFeeling.Services;
using Microsoft.AspNetCore.Mvc;

namespace DailyFeeling.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest user)
    {
        var errorMessage = await _authService.RegisterAsync(user);
        if (errorMessage != null)
        {
            return BadRequest(errorMessage);
        }

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var errorMessage = await _authService.LoginAsync(loginRequest);
        if (errorMessage != null)
        {
            return Unauthorized(errorMessage);
        }

        return Ok("Login bem-sucedido.");
    }
}
