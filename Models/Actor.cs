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
        public string firstName { get; protected set; }
        public string lastName { get; protected set; }
        public string socialSecurityNumber { get; protected set; }
        public Address Address { get; protected set; }


        public Actor(string firstName, string lastName, string socialSecurityNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.socialSecurityNumber = socialSecurityNumber;
        }
        public Actor(string firstName, string lastName, string socialSecurityNumber, Address address)
             : this(firstName, lastName, socialSecurityNumber)
        {
            this.Address = address;
        }
    }
}