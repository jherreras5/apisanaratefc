using Microsoft.EntityFrameworkCore;

namespace api_sanarate.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Define el DbSet para la tabla 'Historias'
        public DbSet<Historia> historia { get; set; }
        // Define el DbSet para la tabla players
        public DbSet<Player> players { get; set; }
        // Define el DbSet para la tabla news
        public DbSet<News> news { get; set; }
        // Define el DbSet para la tabla news
        public DbSet<Team> teams { get; set; }
        public DbSet<Match> matches { get; set; } // Agregar DbSet para la tabla Matches



    }
}
