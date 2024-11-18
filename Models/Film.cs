using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    class Film
    {
        public int Id { get; protected set; }
        public string Titel { get; protected set; }
        public string Beskrivning { get; protected set; }
        public string År { get; protected set; }
        public string Genre { get; protected set; }
        public string Regissör { get; protected set; }
        public int FilmCompanyId { get; protected set; }
        public FilmCompany FilmCompany { get; protected set; }


        //public Film(string titel, string beskrivning, string år, string genre, string regissör, FilmCompany filmCompany)
        //    : this(titel, beskrivning, år, genre, regissör)
        //{
        //    FilmCompany = filmCompany;
        //}

        public Film(string titel, string beskrivning, string år, string genre, string regissör)
        {
            Titel = titel;
            Beskrivning = beskrivning;
            År = år;
            Genre = genre;
            Regissör = regissör;
        }
    }
}