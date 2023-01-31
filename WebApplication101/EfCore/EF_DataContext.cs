using Microsoft.EntityFrameworkCore;

namespace WebApplication101.EfCore
{
    public class EF_DataContext : DbContext
    {
        public EF_DataContext(DbContextOptions<EF_DataContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Lobby> Lobbies { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}
