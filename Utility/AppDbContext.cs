using FilmFolio.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace FilmFolio.Utility
{
    public class AppDbContext : IdentityDbContext
    {//yeni database açarken(code first yaparak) buraya dbsetleniyor
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MovieGenre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Favorite> FavoriteList { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}
