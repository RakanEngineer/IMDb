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
        public Film Film { get; protected set; }
        public FilmCompany(string name)
        {
            Name = name;
        }
        public List<Film> Movies { get; protected set; } = new List<Film>();
        //public FilmCompany(string name, Film film)
        //    : this(name)
        //{
        //    Film = film;
        //}
    }



}