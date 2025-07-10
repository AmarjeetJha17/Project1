using DrugAbuseReportingSystem.Models;
using DrugAbuseReportingSystem.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace DrugAbuseReportingSystem.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAdminService _adminService;

        public AccountController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
               //var isValid = await _adminService.ValidateLoginAsync(model.Username, model.Password);
                //if (isValid)
                //{
                    var admin = await _adminService.GetAdminByUsernameAsync(model.Username);
                    if (admin != null)
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, admin.Username),
                            new Claim(ClaimTypes.NameIdentifier, admin.AdminId.ToString()),
                            new Claim(ClaimTypes.Role, "Admin")
                        };

                        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var authProperties = new AuthenticationProperties
                        {
                            IsPersistent = model.RememberMe
                        };

                        await HttpContext.SignInAsync(
                            CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity),
                            authProperties);

                        return RedirectToAction("Dashboard", "Admin");
                    }
                //}
                //ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }

    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}