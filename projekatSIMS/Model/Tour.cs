using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class Tour : Entity
    {
        private string name;
        private Location location;
        private Language language;
        private DateTime startingDate;
        private string startingTime;
        private int maxNumberOfGuests;
        private int duration;
        private List<KeyPoints> keyPoints;
        private List<string> picturesUrl;
        private string description;

        public Tour() {
            this.location = new Location();
        }

        public Tour(Tour tour,int id)
        {
            this.id = id;
            this.picturesUrl = new List<string>();
            this.name = tour.name;
            this.location = tour.location;
            this.language = tour.language;
            this.startingDate = tour.startingDate;
            this.startingTime = tour.startingTime;
            this.maxNumberOfGuests = tour.maxNumberOfGuests; 
            this.duration = tour.duration;
            this.keyPoints = new List<KeyPoints>();
            this.description = tour.description;
        }

        public string Name 
        { get { return name;}
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

        public Language Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public DateTime StartingDate
        {
            get { return startingDate;}
            set
            {
                startingDate = value;
                OnPropertyChanged(nameof(StartingDate));
            }
        }

        public string StartingTime
        {
            get { return startingTime; }
            set
            {
                startingTime = value;
                OnPropertyChanged(nameof(StartingTime));
            }
        }

        public int MaxNumberOfGuests 
        {
            get { return maxNumberOfGuests; }
            set
            {
                maxNumberOfGuests = value;
                OnPropertyChanged(nameof(MaxNumberOfGuests));
            } 
        }

        public int Duration
        {
            get { return duration; }
            set
            {
                duration = value;
                OnPropertyChanged(nameof(Duration));
            }
        }

        public List<KeyPoints> KeyPoints
        {
            get { return keyPoints; }
            set
            {
                keyPoints = value;
                OnPropertyChanged(nameof(KeyPoints));
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + name + "|" + location.country + "|" + location.city + "|" + language + "|" + startingDate.ToString("dd.MM.yyyy") + "|" + startingTime + "|" + maxNumberOfGuests + "|" + duration;// + "|" + description; // what else is needed
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Name = parts[1];
            location.Country = parts[2];
            location.City = parts[3];
            SetLanguage(parts[4]);
            StartingDate = DateTime.Parse(parts[5], new CultureInfo("de-DE"));
            StartingTime = parts[6];
            MaxNumberOfGuests = int.Parse(parts[7]);
            Duration = int.Parse(parts[8]);
            //Description = parts[9]; 

        }

        public void SetLanguage(string part)
        {
            switch (part)
            {
                case "ENGLISH":
                    Language = Language.ENGLISH;
                    break;

                case "SERBIAN":
                    Language = Language.SERBIAN;
                    break;

                
            }
        }
    }
}
