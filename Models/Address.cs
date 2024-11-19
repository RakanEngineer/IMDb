using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMDb.Models
{
    public class Address
    {
        public int Id { get; protected set; }
        public string street { get; protected set; }
        public string city { get; protected set; }
        public string postcode { get; protected set; }
        //public int ActorId { get; protected set; }

        public Address(string street, string city, string postcode)
        {
            this.street = street;
            this.city = city;
            this.postcode = postcode;
        }
    }
}