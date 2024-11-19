using IMDb.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Data
{
    internal class IMDbContext : DbContext
    {

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<FilmCompany> FilmCompanies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=IMDb;Integrated Security=true; Encrypt=True;Trust Server Certificate=True");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Många-till-många-relation mellan Actor och Movie
            modelBuilder.Entity<MovieActor>()
             .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);


            // En-till-många-relation mellan Movie och FilmCompany
            modelBuilder.Entity<Movie>()
                .HasOne(m => m.FilmCompany)
                .WithMany(fc => fc.Movies)
                .HasForeignKey(m => m.FilmCompanyId);
        }
    }
}