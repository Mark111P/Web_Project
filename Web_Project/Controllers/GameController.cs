using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web_Project.Controllers
{
    public class GameController : Controller
    {
        [Authorize]
        public IActionResult Play()
        {
            string login = User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            ViewData["login"] = login;
            return View("Play");
        }
        [Authorize]
        public IActionResult Records()
        {
            string login = User.Claims.First(i => i.Type == ClaimTypes.Name).Value;
            ViewData["login"] = login;
            return View("Records");
        }
    }
}
