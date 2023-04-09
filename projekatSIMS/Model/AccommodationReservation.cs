using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace projekatSIMS.Model
{
    public class AccommodationReservation : Entity
    {
        private string accommodationName;
        private DateTime startDate;
        private DateTime endDate;
        private int guestCount;
        private bool guestsRate;
        private bool ownersRate;

        public AccommodationReservation()
        {

        }

        public AccommodationReservation(string accommodationName, DateTime startDate, DateTime endDate, int guestCount)
        {
            this.accommodationName = accommodationName;
            this.startDate = startDate;
            this.endDate = endDate;
            this.guestCount = guestCount;
            this.guestsRate = false;
            this.ownersRate = false;
        }

        public string AccommodationName
        {
            get { return accommodationName; }
            set
            {
                accommodationName = value;
                OnPropertyChanged(nameof(Accommodation));
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

        public int GuestCount
        {
            get { return guestCount; }
            set
            {
                guestCount = value;
                OnPropertyChanged(nameof(GuestCount));
            }
        }

        public bool GuestsRate
        {
            get { return guestsRate; }
            set
            {
                guestsRate = value;
                OnPropertyChanged(nameof(GuestsRate));
            }
        }

        public bool OwnersRate
        {
            get { return ownersRate; }
            set
            {
                ownersRate = value;
                OnPropertyChanged(nameof(OwnersRate));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + accommodationName + "|" + startDate.ToString("yyyy-MM-dd") + "|" + endDate.ToString("yyyy-MM-dd") + "|" + guestCount + "|" + guestsRate + "|" + ownersRate;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);  
            accommodationName = parts[1];
            startDate = DateTime.ParseExact(parts[2], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            endDate = DateTime.ParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture);
            guestCount = int.Parse(parts[4]);
            guestsRate = bool.Parse(parts[5]);
            ownersRate = bool.Parse(parts[6]);
        }


    }
}
