using DailyFeeling.DTOs;
using DailyFeeling.Models;
using DailyFeeling.Repositories;
using DailyFeeling.Utils;

namespace DailyFeeling.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ApiResponse<User?>> RegisterAsync(RegisterRequest userRequest)
    {
        if (await _userRepository.EmailExistsAsync(userRequest.Email))
        {
            return ApiResponse<User?>.ErrorResponse(409, "Email already exists");
        }

        if (await _userRepository.UsernameExistsAsync(userRequest.Username))
        {
            return ApiResponse<User?>.ErrorResponse(409, "Username already exists");
        }

        var user = new User
        {
            Username = userRequest.Username,
            Email = userRequest.Email
        };

        user.PasswordHash = PasswordHasher.HashPassword(userRequest.Password);
        user.CreatedAt = DateTime.Now;

        await _userRepository.AddUserAsync(user);
        return ApiResponse<User?>.SuccessResponse(user, "User created");
    }

    public async Task<ApiResponse<User?>> LoginAsync(LoginRequest loginRequest)
    {
        User? user = null;
        if (loginRequest.Email is not null)
        {
            user = await _userRepository.GetUserByEmailAsync(loginRequest.Email);
        }
        else if (loginRequest.Username is not null)
        {
            user = await _userRepository.GetUserByUsernameAsync(loginRequest.Username);
        }

        if (user == null)
        {
            return ApiResponse<User?>.Unauthorized("Invalid Email or Username");
        }

        if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
        {
            return ApiResponse<User?>.Unauthorized("Invalid Password");
        }

        return ApiResponse<User?>.SuccessResponse(user, "Login successful");
    }
}