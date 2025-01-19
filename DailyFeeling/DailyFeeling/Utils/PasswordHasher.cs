using System;
using System.Security.Cryptography;
using System.Text;

namespace DailyFeeling.Utils;

public static class PasswordHasher
{
    // Gera o hash da senha
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(passwordBytes);
        return Convert.ToBase64String(hashBytes);
    }

    // Verifica se a senha corresponde ao hash
    public static bool VerifyPassword(string password, string hash)
    {
        var hashedPassword = HashPassword(password);
        return hashedPassword == hash;
    }
}