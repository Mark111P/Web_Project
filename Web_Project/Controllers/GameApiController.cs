using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Web_Project.Models.Db;
using Web_Project.Services;

namespace Web_Project.Controllers
{
    public class GameApiController : Controller
    {
        private GameContextService dbService;
        public GameApiController(GameContext context)
        {
            dbService = new GameContextService(context);
        }
        public async Task<string> GetRandom(int pairCount)
        {
            List<int> random = await dbService.GetRandomAsync(pairCount);
            var obj = new { random = random };
            return JsonSerializer.Serialize(obj);
        }
        public async Task AddRecord(string login, int pairCount, int attemptCount, string time)
        {
            await dbService.AddRecordAsync(login, pairCount, attemptCount, time);
        }
        public async Task<string> GetRecords(string login, string orderBy, int pairCount)
        {
            List<Record> records = await dbService.GetRecordsAsync(login, orderBy, pairCount);
            var loginRecords = records.Select(i => new {
                login = dbService.GetLoginById(i.UserId),
                attempts = i.AttemptCount,
                gameType = dbService.GetGameTypeString(i.PairCount),
                time = dbService.GetTimeString(i.Time)
            }).ToList();
            var obj = new { records = loginRecords };
            return JsonSerializer.Serialize(obj);
        }
    }
}
