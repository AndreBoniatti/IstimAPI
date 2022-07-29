using IstimAPI.Models;
using IstimAPI.Seeders;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IstimAPI.Data
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<AgeRange> AgeRanges { get; set; }
        public DbSet<UserGame> UserGames { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new SeederCategory());
            builder.ApplyConfiguration(new SeederAgeRange());
            base.OnModelCreating(builder);
        }
    }
}