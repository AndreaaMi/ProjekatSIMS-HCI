using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class AccommodationOwnerRating : Entity
    {
        private int reservationId;
        private int cleanliness;
        private int ownerPoliteness;
        private string comment;
        private string imageUrl;

        public AccommodationOwnerRating()
        {

        }
            public AccommodationOwnerRating(int reservationId, int cleanliness, int ownerPoliteness, string comment)
        {
            this.reservationId = reservationId;
            this.cleanliness = cleanliness;
            this.ownerPoliteness = ownerPoliteness;
            this.comment = comment;
        }

        public int ReservationId
        {
            get { return reservationId; }
            set 
            { 
                reservationId = value; 
                OnPropertyChanged(nameof(ReservationId));
            }

        }
        public int Cleanliness 
        { 
            get { return cleanliness; }
            set 
            {
                cleanliness = value;
                OnPropertyChanged(nameof(Cleanliness));
            }
        }

        public int OwnerPoliteness
        {
            get { return ownerPoliteness; }
            set
            {
                ownerPoliteness = value;
                OnPropertyChanged(nameof(OwnerPoliteness));
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }

        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                imageUrl = value;
                OnPropertyChanged(nameof(ImageUrl));
            }

        }

        public override string ExportToString()
        {
            return id + "|" + reservationId + "|" + cleanliness + "|" + ownerPoliteness + "|" + comment + "|" + imageUrl;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            reservationId = int.Parse(parts[1]);
            cleanliness = int.Parse(parts[2]);
            ownerPoliteness = int.Parse(parts[3]);
            comment = parts[4];
            imageUrl = parts[5];
        }
    }
}
