using DrugAbuseReportingSystem.Models;

namespace DrugAbuseReportingSystem.Services
{
    public interface IReportService
    {
        Task<int> CreateReportAsync(Report report);
        Task<IEnumerable<Report>> GetAllReportsAsync();
        Task<Report?> GetReportByIdAsync(int id);
        Task UpdateReportAsync(Report report);
        Task DeleteReportAsync(int id);
        Task<IEnumerable<Report>> SearchReportsAsync(string searchTerm, DateTime? fromDate, DateTime? toDate, DrugType? drugType, SeverityLevel? severity);
        Task<byte[]> GeneratePdfReportAsync(IEnumerable<Report> reports);
        Task<byte[]> GenerateExcelReportAsync(IEnumerable<Report> reports);
    }
}