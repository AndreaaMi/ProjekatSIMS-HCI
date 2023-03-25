using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private int guestNumber;

        public Tour() {
            this.location = new Location();
            this.keyPoints = new List<KeyPoints>();
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

        public int GuestNumber
        {
            get { return guestNumber; }
            set
            {
                guestNumber = value;
                OnPropertyChanged(nameof(GuestNumber));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + name + "|" + location.country + "|" + location.city + "|" + language + "|" + startingDate.ToString("dd.MM.yyyy") + "|" + startingTime + "|" + maxNumberOfGuests + "|" + duration + "|" + description + "|" + guestNumber + "|" + ExportKeyPoints(keyPoints); // what else is needed
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
            Description = parts[9];
            GuestNumber = int.Parse(parts[10]);
            // KeyPoints = parts[11];
            string[] keyPointIds = parts[11].Split('_');
            ImportKeyPoints(keyPointIds);
            

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

        public string ExportKeyPoints(List<KeyPoints> kp)
        {
            if (kp.Count == 0)
            {
                return "0";
            }

            string keyPointId = (kp.First()).Id.ToString();

            foreach (KeyPoints it in kp)
            {
                if (!(it.Id.ToString().Equals(keyPointId)))
                {
                    keyPointId = keyPointId + "_" + it.Id.ToString();
                }
            }

            return keyPointId;
        }

        public void ImportKeyPoints(string[] keyPointIds)
        {
            foreach (string keyPointId in keyPointIds)
            {
                KeyPoints keyPoints = new KeyPoints
                {
                    Id = int.Parse(keyPointId)
                };

                KeyPoints.Add(keyPoints);
            }


        }

        // public KeyPoints AddKeypoint(int id, string name)
        // {
        //     KeyPoints kp = new KeyPoints();
        //
        // }
    }
}
