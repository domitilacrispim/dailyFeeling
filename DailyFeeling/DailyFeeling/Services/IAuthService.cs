using DailyFeeling.DTOs;

namespace DailyFeeling.Services;

public interface IAuthService
{
	Task<string?> RegisterAsync(RegisterRequest userRequest);
	Task<string?> LoginAsync(LoginRequest loginRequest);
}