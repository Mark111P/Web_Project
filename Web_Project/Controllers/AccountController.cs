using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Web_Project.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View("Login");
        }
        public IActionResult Register()
        {
            return View("Register");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
        public async Task<IActionResult> Authorize(string login)
        {
            var claims = new List<Claim> { new Claim(ClaimTypes.Name, login) };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

            return RedirectToAction("Play", "Game");
        }
    }
}
