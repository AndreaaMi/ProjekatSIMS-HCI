using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class Location : Entity
    {

        public string city;
        public string country;

        

        public Location() { }

        public Location(Location location) 
        { 
            this.city = location.city;
            this.country = location.country;
        }

        public string City
        {
            get { return city; }

            set
            {
                city = value;
                OnPropertyChanged(nameof(City));
            }
        }
        public string Country
        {
            get { return country; }

            set
            {
                country = value;
                OnPropertyChanged(nameof(Country));

            }
        }

        public override string ExportToString()
        {
            return id + "|" + country + "|" + city;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Country = parts[1];
            City = parts[2];
        }


    }
}
