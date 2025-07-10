using DrugAbuseReportingSystem.Models;

namespace DrugAbuseReportingSystem.Services
{
    public interface IAdminService
    {
        Task<bool> ValidateLoginAsync(string username, string password);
        Task<AdminUser?> GetAdminByUsernameAsync(string username);
       
    }
}