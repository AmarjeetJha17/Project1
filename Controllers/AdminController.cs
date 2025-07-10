using DrugAbuseReportingSystem.Models;
using DrugAbuseReportingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DrugAbuseReportingSystem.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IReportService _reportService;
        private readonly IAdminService _adminService;

        public AdminController(IReportService reportService, IAdminService adminService)
        {
            _reportService = reportService;
            _adminService = adminService;
        }

        public async Task<IActionResult> Dashboard()
        {
            var reports = await _reportService.GetAllReportsAsync();
            return View(reports);
        }

        public async Task<IActionResult> ReportDetails(int id)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateReportStatus(int id, ReportStatus status, string adminNotes)
        {
            var report = await _reportService.GetReportByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            report.Status = status;
            report.AdminNotes = adminNotes;

            await _reportService.UpdateReportAsync(report);
            return RedirectToAction("ReportDetails", new { id });
        }

        public async Task<IActionResult> SearchReports(string searchTerm, DateTime? fromDate, DateTime? toDate, DrugType? drugType, SeverityLevel? severity)
        {
            var reports = await _reportService.SearchReportsAsync(searchTerm, fromDate, toDate, drugType, severity);
            return View("Dashboard", reports);
        }

        public async Task<IActionResult> GeneratePdfReport()
        {
            var reports = await _reportService.GetAllReportsAsync();
            var pdfBytes = await _reportService.GeneratePdfReportAsync(reports);

            // Return as PDF file
            return File(pdfBytes, "application/pdf", $"DrugAbuseReports_{DateTime.Now:yyyyMMdd}.pdf");
        }

        public async Task<IActionResult> GenerateExcelReport()
        {
            var reports = await _reportService.GetAllReportsAsync();
            var excelBytes = await _reportService.GenerateExcelReportAsync(reports);
            return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", $"DrugAbuseReports_{DateTime.Now:yyyyMMdd}.xlsx");
        }
    }
}