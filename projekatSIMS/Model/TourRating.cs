using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Model
{
    public class TourRating : Entity
    {
        private int touristId;
        private int tourId;
        private int tourGuideKnowledge;
        private int tourGuideLanguageProficiency;
        private int interestLevel;
        private string comment;
        private string imageUrl;

        public TourRating()
        {

        }

        public TourRating(int id,int tourId,int touristId,int knowledge,int language, int interest, string comment, string imageUrl)
        {
            this.id = id;
            this.tourId = tourId;
            this.touristId = touristId;
            this.tourGuideKnowledge = knowledge;
            this.tourGuideLanguageProficiency = language;
            this.interestLevel = interest;
            this.comment = comment;
            this.imageUrl = imageUrl;
        }

        public int TouristId
        {
            get { return touristId; }
            set
            {
                touristId = value;
                OnPropertyChanged(nameof(TouristId));
            }
        }

        public int TourId
        {
            get { return tourId; }
            set
            {
                tourId = value;
                OnPropertyChanged(nameof(TourId));
            }
        }

        public int TourGuideKnowledge
        {
            get { return tourGuideKnowledge; }
            set
            {
                tourGuideKnowledge = value;
                OnPropertyChanged(nameof(TourGuideKnowledge));
            }
        }

        public int TourGuideLanguageProficiency
        {
            get { return tourGuideLanguageProficiency; }
            set
            {
                tourGuideLanguageProficiency= value;
                OnPropertyChanged(nameof(TourGuideLanguageProficiency));
            }
        }

        public int InterestLevel
        {
            get { return interestLevel;}
            set
            {
                interestLevel = value;
                OnPropertyChanged(nameof(InterestLevel));
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
            return id + "|" + tourId + "|" + touristId + "|" + tourGuideKnowledge + "|" + tourGuideLanguageProficiency + "|" + interestLevel + "|" + comment + "|" + imageUrl;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            tourId = int.Parse(parts[1]);
            touristId = int.Parse(parts[2]);
            tourGuideKnowledge = int.Parse(parts[3]);
            tourGuideLanguageProficiency = int.Parse(parts[4]);
            interestLevel = int.Parse(parts[5]);
            comment = parts[6];
            imageUrl = parts[7];
        }
    }
}
