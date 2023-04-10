using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class TourReservation : Entity
    {
        private int guestId;
        private int tourId;
        private int numberOfGuests;

        public TourReservation()
        {

        }

        public TourReservation(int id,int guestId, int tourId,int numberOfGuests)
        {
            this.id = id;
            this.guestId = guestId;
            this.tourId = tourId;
            this.numberOfGuests = numberOfGuests;
        }

        public int GuestId
        {
            get { return guestId; }
            set { 
                guestId = value; 
                OnPropertyChanged(nameof(guestId));
            }
        }

        public int TourId
        {
            get { return tourId; }
            set
            {
                tourId = value;
                OnPropertyChanged(nameof(tourId));
            }
        }

        public int NumberOfGuests
        {
            get { return numberOfGuests; }
            set
            {
                numberOfGuests = value;
                OnPropertyChanged(nameof(numberOfGuests));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + guestId + "|" + tourId + "|" + numberOfGuests;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            guestId = int.Parse(parts[1]);
            tourId= int.Parse(parts[2]);
            numberOfGuests= int.Parse(parts[3]);
        }
    }
}
