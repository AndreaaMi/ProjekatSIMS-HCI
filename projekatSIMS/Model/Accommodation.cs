using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    internal class Accommodation : Entity
    {

        private string name;
        private Location location;
        private AccommodationType type;
        private int guestLimit;
        private int minimalStay;
        private int cancelationLimit;
    

            public Accommodation() { }

        public Accommodation(string name, Location location, AccommodationType accommodationType, int guestLimit, int minimalStay, int cancelationLimit)
        {
            this.name = name;
            this.location = location;
            this.type = accommodationType;
            this.guestLimit = guestLimit;
            this.minimalStay = minimalStay;
            this.cancelationLimit = cancelationLimit;
        }




        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged(nameof(Location));
            }

        }

        public AccommodationType Type
        {
            get { return type; }
            set
            {
                type = value;
                OnPropertyChanged(nameof(Type));
            }
        }

        public void SetAccommodationType(string part)
        {
            switch (part)
            {
                case "APARTMENT":
                    Type = AccommodationType.APARTMENT;
                    break;

                case "HOUSE":
                    Type = AccommodationType.HOUSE;
                    break;

                case "COTTAGE":
                    Type = AccommodationType.COTTAGE;
                    break;
            }
        }

        public int GuestLimit
        {
            get { return guestLimit; }
            set
            {
                guestLimit = value;
                OnPropertyChanged(nameof(GuestLimit));
            }
        }

        public int MinimalStay
        {
            get { return minimalStay; }
            set
            {
                minimalStay = value;
                OnPropertyChanged(nameof(MinimalStay));
            }
        }

        public int CancelationLimit
        {
            get { return cancelationLimit; }
            set
            {
                cancelationLimit = value;
                OnPropertyChanged(nameof(CancelationLimit));
            }
        }



        public override string ExportToString()
        {
            return id + "|" + name + "|" + type.ToString() + "|" + guestLimit + "|" + minimalStay + "|" + cancelationLimit;
        }

       
    }
}
  
