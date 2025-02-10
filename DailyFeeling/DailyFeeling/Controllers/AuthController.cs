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
    private readonly JwtService _jwtService;

    public AuthController(IAuthService authService, JwtService jwtService)
    {
        _authService = authService;
        _jwtService = jwtService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest user)
    {
        var response = await _authService.RegisterAsync(user);
        return StatusCode(response.StatusCode, response);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
    {
        var response = await _authService.LoginAsync(loginRequest);
        if (response.Success && response.Data is User)
        {
            var token = _jwtService.GenerateToken(response.Data);
            return Ok(new { token = token, response = response });
        }
        return StatusCode(response.StatusCode, response);
    }
}
