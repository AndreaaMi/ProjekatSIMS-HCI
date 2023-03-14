using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class Location
    {
        public string city;
        public string country;

        public Location()
        {
        }

        public Location(string city, string country)
        {
            City = city;
            Country = country;
        }

        public string City
        {
            get; set;
        }

        public string Country
        {
            get; set;
        }

    }
}
