using System;
using DailyFeeling.DTOs;
using DailyFeeling.Models;
using DailyFeeling.Repositories;
using DailyFeeling.Utils;

namespace DailyFeeling.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;

        public AuthService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string?> RegisterAsync(RegisterRequest userRequest)
        {
            if (await _userRepository.EmailExistsAsync(userRequest.Email))
            {
                return "Email já cadastrado.";
            }

            if (await _userRepository.UsernameExistsAsync(userRequest.Username))
            {
                return "Username já cadastrado";
            }

            var user = new User
            {
                Username = userRequest.Username,
                Email = userRequest.Email
            };

            user.PasswordHash = PasswordHasher.HashPassword(userRequest.Password);
            user.CreatedAt = DateTime.Now;

            await _userRepository.AddUserAsync(user);
            return null; // Indica sucesso
        }

        public async Task<string?> LoginAsync(LoginRequest loginRequest)
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
                return "Email ou Username inválido.";
            }

            if (!PasswordHasher.VerifyPassword(loginRequest.Password, user.PasswordHash))
            {
                return "Senha inválida!";
            }

            return null; // Indica sucesso
        }
    }

}

