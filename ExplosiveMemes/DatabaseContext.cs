using ExplosiveMemes.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExplosiveMemes
{
    public class DatabaseContext : DbContext
    {
        public DbSet<BotUser> Users { get; set; }

        public DatabaseContext()
        {
           var created = Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=homeless;Trusted_Connection=True;");
        }
    }
}
