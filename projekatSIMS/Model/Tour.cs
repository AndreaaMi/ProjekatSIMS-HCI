using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    internal class Tour : Entity
    {
        private string name;
        private Location location;
        private Language language;
        private DateTime startingTime;
        private int maxNumberOfGuests;
        private double duration;
        private List<KeyPoints> keyPoints;
        private List<string> picturesUrl;

        public Tour() { }

        public Tour(Tour tour,int id)
        {
            this.id = id;
            this.picturesUrl = new List<string>();
            this.name = tour.name;
            this.location = tour.location;
            this.language = tour.language;
            this.startingTime = tour.startingTime;
            this.maxNumberOfGuests = tour.maxNumberOfGuests; 
            this.duration = tour.duration;
            this.keyPoints = new List<KeyPoints>();
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

        public DateTime StartingTime
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

        public double Duration
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

        public override string ExportToString()
        {
            return id + "|" + name; // what else is needed
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            Name = parts[1];
            //Location = parts[2]; // what else is needed


        }
    }
}
