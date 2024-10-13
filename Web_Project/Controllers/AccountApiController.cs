using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using Web_Project.Models.Db;
using Web_Project.Services;

namespace Web_Project.Controllers
{
    public class AccountApiController : Controller
    {
        private GameContextService dbService;
        public AccountApiController(GameContext context)
        {
            dbService = new GameContextService(context);
        }
        public async Task<string> CheckLogin(string login, string password)
        {
            string message = await dbService.CheckLoginAsync(login, password);
            if (message == "ok")
            {
                var obj = new
                {
                    message = message,
                    login = login
                };
                return JsonSerializer.Serialize(obj);
            }
            else
            {
                var obj = new
                {
                    message = message
                };
                return JsonSerializer.Serialize(obj);
            }
        }

        public async Task<string> CheckRegister(string login, string password)
        {
            string message = await dbService.CheckRegisterAsync(login, password);

            var obj = new
            {
                message = message
            };
            return JsonSerializer.Serialize(obj);
        }
    }
}
