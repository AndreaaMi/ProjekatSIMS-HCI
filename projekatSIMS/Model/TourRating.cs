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
        private int tourGuideKnowledge;
        private int tourGuideLanguageProficiency;
        private int interestLevel;
        private string comment;
        private string imageUrl;

        public TourRating()
        {

        }

        public TourRating(int id,int touristId,int knowledge,int language, int interest, string comment, string imageUrl)
        {
            this.id = id;
            this.touristId = id;
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
            return id + "|" + touristId + "|" + tourGuideKnowledge + "|" + tourGuideLanguageProficiency + "|" + interestLevel + "|" + comment + "|" + imageUrl;
        }

        public override void ImportFromString(string[] parts)
        {
            base.ImportFromString(parts);
            touristId = int.Parse(parts[1]);
            tourGuideKnowledge = int.Parse(parts[2]);
            tourGuideLanguageProficiency = int.Parse(parts[3]);
            interestLevel = int.Parse(parts[4]);
            comment = parts[5];
            imageUrl = parts[6];
        }
    }
}
