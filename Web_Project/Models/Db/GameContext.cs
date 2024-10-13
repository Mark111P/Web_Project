using Microsoft.EntityFrameworkCore;

namespace Web_Project.Models.Db
{
    public class GameContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Record> Records { get; set; }
        public GameContext(DbContextOptions<GameContext> options) : base(options) { Database.EnsureCreated(); }
    }
}
