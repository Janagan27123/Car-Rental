using CarRental.Models;

namespace CarRental.Services
{
    public interface IAdminService
    {
        Task<AdminUser?> AuthenticateAsync(string username, string password);
        Task<bool> ValidatePasswordAsync(string password, string hash);
        Task<string> HashPasswordAsync(string password);
        Task<AdminUser?> GetAdminByUsernameAsync(string username);
    }
} 