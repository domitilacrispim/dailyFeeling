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
    
    [HttpPost("File")]
    public async Task<IActionResult> File()
    {
        try
        {
            // Diretório atual onde o executável está sendo executado
            string currentDirectory = Directory.GetCurrentDirectory();
        
            // Nome do arquivo a ser criado
            string fileName = "arquivo.txt";
        
            // Caminho completo do arquivo
            string filePath = Path.Combine(currentDirectory, fileName);
        
            // Conteúdo do arquivo
            string content = "Este é o conteúdo do arquivo criado via endpoint.";
        
            // Criação do arquivo e escrita do conteúdo
            await System.IO.File.WriteAllTextAsync(filePath, content);
        
            return Ok($"Arquivo '{fileName}' criado no diretório: {currentDirectory}");
        }
        catch (Exception ex)
        {
            // Captura e retorna qualquer erro ocorrido
            return StatusCode(500, $"Erro ao criar o arquivo: {ex.Message}");
        }
    }
}
