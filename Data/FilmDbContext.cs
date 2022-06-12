using Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Data
{
    public class FilmDbContext:IdentityDbContext
    {
        public FilmDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }  
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Language> Languages { get; set; }  
        public DbSet<Genre> Genres { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=DESKTOP-4UEUNLL\\LEHAMAISAK; Database=FilmDataBase; Trusted_connection=True");
        }
    }
}
