using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    class FilmCompany
    {
        public int Id { get; protected set; }
        public string Name { get; protected set; }
        //public ICollection<Movie> Movies { get; set; }
       // public Movie Movie { get; protected set; }
        public List<Movie> Movies { get; protected set; } = new List<Movie>();

        public FilmCompany(string name)
        {
            Name = name;
        }
        //public FilmCompany(string name, Film film)
        //    : this(name)
        //{
        //    Film = film;
        //}
    }



}