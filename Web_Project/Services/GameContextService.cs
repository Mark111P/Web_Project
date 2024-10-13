using Web_Project.Models.Db;

namespace Web_Project.Services
{
    public class GameContextService
    {
        private GameContext context;
        public GameContextService(GameContext context) { this.context = context; }
        public async Task<string> CheckLoginAsync(string login, string password)
        {
            string message = "";
            List<User> users = context.Users.ToList();
            if (users.Any(i => i.Login == login))
            {
                if (users.First(i => i.Login == login).Password == password)
                {
                    message = "ok";
                }
                else
                {
                    message = "passwordError";
                }
            }
            else
            {
                message = "loginError";
            }
            return message;
        }
        public async Task<string> CheckRegisterAsync(string login, string password)
        {
            string message = "";
            List<User> users = context.Users.ToList();

            if (!users.Any(i => i.Login == login))
            {
                User user = new User()
                {
                    Login = login,
                    Password = password
                };
                context.Users.Add(user);
                context.SaveChanges();

                message = "ok";
            }
            else
            {
                message = "loginError";
            }
            return message;
        }

        public async Task<List<int>> GetRandomAsync(int pairCount)
        {
            List<int> result = new List<int>();

            List<int> pairs = await GetRandomPairsAsync(pairCount);
            pairs.AddRange(pairs);
            Random random = new Random();
            int randomCount = pairCount * 2;

            for (int i = 0; i < randomCount; i++)
            {
                int index = random.Next(pairs.Count);
                int element = pairs[index];
                pairs.RemoveAt(index);
                result.Add(element);
            }

            return result;
        }
        public async Task<List<int>> GetRandomPairsAsync(int pairCount)
        {
            List<int> result = new List<int>();

            int maxPairs = 18;
            int removeCount = maxPairs - pairCount;
            Random random = new Random();

            for (int i = 0; i < maxPairs; i++)
            {
                result.Add(i + 1);
            }
            for (int i = 0; i < removeCount; i++)
            {
                result.RemoveAt(random.Next(result.Count));
            }

            return result;
        }
        public async Task AddRecordAsync(string userLogin, int pairCount, int attemptCount, string timeString)
        {
            int userId = context.Users.First(i => i.Login == userLogin).Id;
            List<int> tmp = timeString.Split(':').Select(int.Parse).ToList();
            TimeSpan time = new TimeSpan(0, 0, tmp[0], tmp[1], tmp[2]);

            Record record = new Record()
            {
                UserId = userId,
                PairCount = pairCount,
                AttemptCount = attemptCount,
                Time = time
            };
            context.Records.Add(record);
            await context.SaveChangesAsync();
        }
        public async Task<List<Record>> GetRecordsAsync(string login, string orderBy, int pairCount)
        {
            List<Record> records = context.Records.ToList().Where(i => i.PairCount == pairCount).ToList();
            if (login != "ALL")
            {
                int userId = context.Users.First(i => i.Login == login).Id;
                records = records.Where(i => i.UserId == userId).ToList();
            }

            if (orderBy == "attempts")
            {
                records = records.OrderBy(i => i.Time).OrderBy(i => i.AttemptCount).ToList();
            }
            else if (orderBy == "time")
            {
                records = records.OrderBy(i => i.AttemptCount).OrderBy(i => i.Time).ToList();
            }
            else if (orderBy == "last")
            {
                records.Reverse();
            }
            return records.Take(10).ToList();
        }
        public string GetLoginById(int id)
        {
            return context.Users.First(i => i.Id == id).Login;
        }
        public string GetTimeString(TimeSpan time)
        {
            string timeString = ((int)time.TotalMinutes).ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();
            return timeString;
        }
        public string GetGameTypeString(int pairCount)
        {
            if (pairCount == 8) return "4x4";
            else if (pairCount == 12) return "4x6";
            else if (pairCount == 18) return "6x6";
            else return "error";
        }
    }
}
