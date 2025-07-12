using Microsoft.EntityFrameworkCore;
using CarRental.Data;
using CarRental.Models;
using System.Security.Cryptography;
using System.Text;

namespace CarRental.Services
{
    public class AdminService : IAdminService
    {
        private readonly CarRentalContext _context;

        public AdminService(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<AdminUser?> AuthenticateAsync(string username, string password)
        {
            var admin = await _context.AdminUsers
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive);

            if (admin == null)
                return null;

            if (await ValidatePasswordAsync(password, admin.PasswordHash))
                return admin;

            return null;
        }

        public async Task<bool> ValidatePasswordAsync(string password, string hash)
        {
            var hashedPassword = await HashPasswordAsync(password);
            return hashedPassword == hash;
        }

        public Task<string> HashPasswordAsync(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Task.FromResult(Convert.ToBase64String(hashedBytes));
            }
        }

        public async Task<AdminUser?> GetAdminByUsernameAsync(string username)
        {
            return await _context.AdminUsers
                .FirstOrDefaultAsync(a => a.Username == username && a.IsActive);
        }
    }
} 