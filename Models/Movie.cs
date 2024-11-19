using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    class Movie
    {
        public int Id { get; protected set; }
        public string Title { get; protected set; }
        public string Description { get; protected set; }
        public string ReleaseYear { get; protected set; }
        public string Genre { get; protected set; }
        public string Director { get; protected set; }
        public int FilmCompanyId { get; protected set; }
        public FilmCompany FilmCompany { get; protected set; }
        public ICollection<MovieActor> MovieActors { get; set; }

        public Movie(string title, string description, string releaseYear, string genre, string director, int filmCompanyId)
            : this(title, description, releaseYear, genre, director)
        {
            FilmCompanyId = filmCompanyId;
        }

        public Movie(string title, string description, string releaseYear, string genre, string director)
        {
            Title = title;
            Description = description;
            ReleaseYear = releaseYear;
            Genre = genre;
            Director = director;
        }
    }
}