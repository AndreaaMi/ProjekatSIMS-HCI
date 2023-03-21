using projekatSIMS.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class GuestReview : Entity
    {
        public AccommodationReservation accommodationReservation;
        AccommodationReservationService accommodationReservationService = new AccommodationReservationService();
        public int cleanliness;
        public int respectingRules;
        public string comment;


        public GuestReview() { }
        public GuestReview(AccommodationReservation accommodationReservation, int cleanliness, int respectingRules, string comment)
        {
            this.accommodationReservation = accommodationReservation;
            this.comment = comment;
            this.cleanliness = cleanliness;
            this.respectingRules = respectingRules;

        }

        public AccommodationReservation AccommodationReservation
        {
            get { return accommodationReservation; }
            set
            {
                accommodationReservation = value;
                OnPropertyChanged(nameof(AccommodationReservation));
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
        public int Cleanliness
        {
            get { return cleanliness; }
            set
            {
                cleanliness = value;
                OnPropertyChanged(nameof(Cleanliness));
            }
        }
        public int RespectingRules
        {
            get { return respectingRules; }
            set
            {
                respectingRules = value;
                OnPropertyChanged(nameof(RespectingRules));
            }
        }


        public override string ExportToString()
        {
            return id + "|" + accommodationReservation.Id + "|" + cleanliness + "|" + respectingRules + "|" + comment;
        }

        public override void ImportFromString(string[] parts)
        {

            base.ImportFromString(parts);
            AccommodationReservation = (AccommodationReservation)accommodationReservationService.Get(int.Parse(parts[1]));
            Cleanliness = int.Parse(parts[2]);
            respectingRules = int.Parse(parts[3]);
            Comment = parts[4];
        }
    }
}

