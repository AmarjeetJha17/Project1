using DrugAbuseReportingSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace DrugAbuseReportingSystem.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationDbContext _context;

        public AdminService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ValidateLoginAsync(string username, string password)
        {
            var admin = await _context.AdminUsers.FirstOrDefaultAsync(a => a.Username == username);
            if (admin == null) return false;

            using var hmac = new HMACSHA512(Encoding.UTF8.GetBytes(admin.PasswordHash));
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));

            return computedHash.SequenceEqual(Encoding.UTF8.GetBytes(admin.PasswordHash));
        }

        public async Task<AdminUser?> GetAdminByUsernameAsync(string username)
        {
            return await _context.AdminUsers.FirstOrDefaultAsync(a => a.Username == username);
        }

    }
}