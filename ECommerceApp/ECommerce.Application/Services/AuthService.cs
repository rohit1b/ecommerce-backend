using System.Security.Cryptography;
using System.Text;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;

namespace ECommerce.Application.Services;

public class AuthService : IAuthService
{
    private readonly IAdminUserRepository _adminUserRepository;

    public AuthService(IAdminUserRepository adminUserRepository)
    {
        _adminUserRepository = adminUserRepository;
    }

    public static string HashPassword(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        return Convert.ToHexString(bytes);
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _adminUserRepository.GetByEmailAsync(dto.Email);

        if (user is null || user.PasswordHash != HashPassword(dto.Password))
        {
            return new LoginResponseDto
            {
                Success = false,
                Message = "Invalid email or password"
            };
        }

        // Simple demo token (not a real JWT). Good enough for this admin panel.
        var token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

        return new LoginResponseDto
        {
            Success = true,
            Message = "Login successful",
            Token = token,
            Email = user.Email
        };
    }
}
