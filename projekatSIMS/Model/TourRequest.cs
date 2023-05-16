using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class TourRequest : Entity
    {
        private int guestId;
        private Location location;
        private string description;
        private string language;
        private int guestNumber;
        private DateTime startDate;
        private DateTime endDate;
        private TourRequestStatus status;
        private string date;

        public TourRequest()
        {
            this.location = new Location();
        }

        public TourRequest(int guestId, Location location, string description, string language, int guestNumber, DateTime startDate, DateTime endDate, TourRequestStatus status, string date)
        {
            GuestId = guestId;
            Location = location;
            Description = description;
            Language = language;
            GuestNumber = guestNumber;
            StartDate = startDate;
            EndDate = endDate;
            Status = status;
            
            Date = date;
        }

        public string Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged(nameof(Date));
            }
        }

        public int GuestId
        {
            get { return guestId; }
            set
            {
                guestId = value;
                OnPropertyChanged(nameof(GuestId));
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

        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                OnPropertyChanged(nameof(Language));
            }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                OnPropertyChanged(nameof(EndDate));
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

        public TourRequestStatus Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
        public override string ExportToString()
        {
            return id + "|" + guestId + "|" + location.country + "|" + location.city + "|" + language + "|" + startDate.ToString("dd.MM.yyyy") + "|" + endDate.ToString("dd.MM.yyyy") + "|" + description + "|" + guestNumber + "|" + status + "|"+ date;
        }
        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            guestId = int.Parse(parts[1]);
            location.Country = parts[2];
            location.City = parts[3];
            language = parts[4];
            StartDate = DateTime.Parse(parts[5], new CultureInfo("de-DE"));
            EndDate = DateTime.Parse(parts[6], new CultureInfo("de-DE"));
            Description = parts[7];
            GuestNumber = int.Parse(parts[8]);
            SetStatus(parts[9]); 
            Date = parts[10];
        }

        public void SetStatus(string part)
        {
            switch (part)
            {
                case "ACCEPTED":
                    Status = TourRequestStatus.ACCEPTED;
                    break;

                case "PENDING":
                    Status = TourRequestStatus.PENDING;
                    break;

                case "REJECTED":
                    Status = TourRequestStatus.REJECTED;
                    break;
            }
        }

    }
}
