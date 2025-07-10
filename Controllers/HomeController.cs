using DrugAbuseReportingSystem.Models;
using DrugAbuseReportingSystem.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DrugAbuseReportingSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var drugInfo = _context.DrugInformation.ToList();
            var rehabCenters = _context.RehabilitationCenters.ToList();

            var model = new HomeViewModel
            {
                DrugInformation = drugInfo,
                RehabilitationCenters = rehabCenters
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Terms()
        {
            return View();
        }

        public IActionResult Guidelines()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

    public class HomeViewModel
    {
        public List<DrugInformation> DrugInformation { get; set; }
        public List<RehabilitationCenter> RehabilitationCenters { get; set; }
    }
}