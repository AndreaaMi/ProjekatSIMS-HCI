using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class Accommodation : Entity
    {

        private string name;
        private int ownerId;
        private Location location;
        private AccommodationType type;
        private int guestLimit;
        private int minimumStayDays;
        private int cancellationDays;
        private List<string> imageUrls;


        public Accommodation()
        {
            this.location = new Location();
        }

        public Accommodation(int ownerId, string name, Location location, AccommodationType accommodationType, int guestLimit, int minimumStayDays, int cancellationDays, List<string> imageUrls)
        {
            this.ownerId = ownerId;
            this.name = name;
            this.location = location;
            this.type = accommodationType;
            this.guestLimit = guestLimit;
            this.minimumStayDays = minimumStayDays;
            this.cancellationDays = cancellationDays;
            this.imageUrls = imageUrls;
        }


        public int OwnerId
        {
            get { return ownerId; }
            set
            {
                ownerId = value;
                OnPropertyChanged(nameof(OwnerId));
            }
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

        public int MinimumStayDays
        {
            get { return minimumStayDays; }
            set
            {
                minimumStayDays = value;
                OnPropertyChanged(nameof(MinimumStayDays));
            }
        }

        public int CancellationDays
        {
            get { return cancellationDays; }
            set
            {
                cancellationDays = value;
                OnPropertyChanged(nameof(CancellationDays));
            }
        }

        public List<string> ImageUrls
        {
            get { return imageUrls; }
            set
            {
                imageUrls = value;
                OnPropertyChanged(nameof(ImageUrls));

            }
        }





        public override string ExportToString()
        {
            string imageUrlsString = string.Join(";", ImageUrls);
            return id + "|" + ownerId + "|" + name + "|" + type.ToString() + "|" + location.City + "|" + location.Country + "|" + guestLimit + "|" + minimumStayDays + "|" + cancellationDays + "|" + imageUrlsString;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);


            OwnerId = int.Parse(parts[1]);
            Name = parts[2];
            SetAccommodationType(parts[3]);
            Location.City = parts[4];
            Location.Country = parts[5];
            GuestLimit = int.Parse(parts[6]);
            MinimumStayDays = int.Parse(parts[7]);
            CancellationDays = int.Parse(parts[8]);
            string[] imageUrlsArray = parts[9].Split(';');
            ImageUrls = new List<string>(imageUrlsArray);



        }



    }
}

