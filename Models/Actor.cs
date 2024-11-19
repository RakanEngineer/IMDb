using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    class Actor
    {
        public int Id { get; protected set; }
        public string FirstName { get; protected set; }
        public string LastName { get; protected set; }
        public string SocialSecurityNumber { get; protected set; }
        public Address Address { get; protected set; }
        public ICollection<MovieActor> MovieActors { get; set; }


        public Actor(string firstName, string lastName, string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }
        public Actor(string firstName, string lastName, string socialSecurityNumber, Address address)
             : this(firstName, lastName, socialSecurityNumber)
        {
            this.Address = address;
        }
    }
}