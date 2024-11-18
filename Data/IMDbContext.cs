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

        public DbSet<Actor> Actor { get; set; }
        public DbSet<FilmCompany> FilmCompany { get; set; }
        public DbSet<Film> Film { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.;Database=IMDb;Integrated Security=true; Encrypt=True;Trust Server Certificate=True");
        }
    }
}