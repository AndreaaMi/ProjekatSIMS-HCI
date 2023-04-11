using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class TourRatingRepository : Repository<TourRating>
    {
        public override void Edit(Entity entity)
        {
            Entity tourRating = base.Get(entity.Id);
            ((TourRating)tourRating).TouristId = ((TourRating)entity).TouristId;
            ((TourRating)tourRating).TourGuideKnowledge = ((TourRating)entity).TourGuideKnowledge;
            ((TourRating)tourRating).TourGuideLanguageProficiency = ((TourRating)entity).TourGuideLanguageProficiency;
            ((TourRating)tourRating).InterestLevel = ((TourRating)entity).InterestLevel;
            ((TourRating)tourRating).Comment = ((TourRating)entity).Comment;
            ((TourRating)tourRating).ImageUrl = ((TourRating)entity).ImageUrl;
        }
    }
}
