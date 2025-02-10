using DailyFeeling.DTOs;
using DailyFeeling.Models;

namespace DailyFeeling.Services;

public interface IAuthService
{
	Task<ApiResponse<User?>> RegisterAsync(RegisterRequest userRequest);
	Task<ApiResponse<User?>> LoginAsync(LoginRequest loginRequest);
}