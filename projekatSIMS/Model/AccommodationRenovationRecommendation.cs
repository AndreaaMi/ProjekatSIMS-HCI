using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class AccommodationRenovationRecommendation : Entity
    {
        private int reservationId;
        private string generalRefurbishment;
        private string bathroomRenovation;
        private string furnitureRenovation;
        private string technicalInstallationsUpgrade;
        private string recreationAreaRefurbishment;
        private string safetyImprovements;
        private string comment;


        public AccommodationRenovationRecommendation()
        {
        }

        public AccommodationRenovationRecommendation(int reservationId, string generalRefurbishment, string bathroomRenovation, string furnitureRenovation,
            string technicalInstallationsUpgrade, string recreationAreaRefurbishment, string safetyImprovements, string comment)
        {
            ReservationId = reservationId;
            GeneralRefurbishment = generalRefurbishment;
            BathroomRenovation = bathroomRenovation;
            FurnitureRenovation = furnitureRenovation;
            TechnicalInstallationsUpgrade = technicalInstallationsUpgrade;
            RecreationAreaRefurbishment = recreationAreaRefurbishment;
            SafetyImprovements = safetyImprovements;
            Comment = comment;
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
        public string GeneralRefurbishment
        {
            get { return generalRefurbishment; }
            set
            {
                generalRefurbishment = value;
                OnPropertyChanged(nameof(GeneralRefurbishment));
            }
        }

        public string BathroomRenovation
        {
            get { return bathroomRenovation; }
            set
            {
                bathroomRenovation = value;
                OnPropertyChanged(nameof(BathroomRenovation));
            }
        }

        public string FurnitureRenovation
        {
            get { return furnitureRenovation; }
            set
            {
                furnitureRenovation = value;
                OnPropertyChanged(nameof(FurnitureRenovation));
            }
        }

        public string TechnicalInstallationsUpgrade
        {
            get { return technicalInstallationsUpgrade; }
            set
            {
                technicalInstallationsUpgrade = value;
                OnPropertyChanged(nameof(TechnicalInstallationsUpgrade));
            }
        }

        public string RecreationAreaRefurbishment
        {
            get { return recreationAreaRefurbishment; }
            set
            {
                recreationAreaRefurbishment = value;
                OnPropertyChanged(nameof(RecreationAreaRefurbishment));
            }
        }

        public string SafetyImprovements
        {
            get { return safetyImprovements; }
            set
            {
                safetyImprovements = value;
                OnPropertyChanged(nameof(SafetyImprovements));
            }
        }

        public string Comment
        {
            get { return comment; }
            set
            {
                comment = value;
                OnPropertyChanged(nameof(comment));
            }
        }

        public override string ExportToString()
        {
            return id + "|" + reservationId + "|" + GeneralRefurbishment + "|" + BathroomRenovation + "|" +
                FurnitureRenovation + "|" + TechnicalInstallationsUpgrade + "|" +
                RecreationAreaRefurbishment + "|" + SafetyImprovements + "|" +
                Comment;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            ReservationId = int.Parse(parts[1]);
            GeneralRefurbishment = parts[2];
            BathroomRenovation = parts[3];
            FurnitureRenovation = parts[4];
            TechnicalInstallationsUpgrade = parts[5];
            RecreationAreaRefurbishment = parts[6];
            SafetyImprovements = parts[7];
            Comment = parts[8];
        }
    }
}
