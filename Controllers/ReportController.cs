using DrugAbuseReportingSystem.Models;
using DrugAbuseReportingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace DrugAbuseReportingSystem.Controllers
{
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Report report)
        {
            if (ModelState.IsValid)
            {
                await _reportService.CreateReportAsync(report);
                return RedirectToAction("Confirmation");
            }
            return View(report);
        }

        public IActionResult Confirmation()
        {
            return View();
        }
    }
}