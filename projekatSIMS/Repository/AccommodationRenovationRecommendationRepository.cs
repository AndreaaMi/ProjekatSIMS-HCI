using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class AccommodationRenovationRecommendationRepository : Repository<AccommodationRenovationRecommendation>
    {
        public override void Edit(Entity entity)
        {
            Entity accommodationRenovationRecommendation = base.Get(entity.Id);

            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).Id = ((AccommodationRenovationRecommendation)entity).Id;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).ReservationId = ((AccommodationRenovationRecommendation)entity).ReservationId;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).GeneralRefurbishment = ((AccommodationRenovationRecommendation)entity).GeneralRefurbishment;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).BathroomRenovation = ((AccommodationRenovationRecommendation)entity).BathroomRenovation;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).FurnitureRenovation = ((AccommodationRenovationRecommendation)entity).FurnitureRenovation;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).TechnicalInstallationsUpgrade = ((AccommodationRenovationRecommendation)entity).TechnicalInstallationsUpgrade;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).RecreationAreaRefurbishment = ((AccommodationRenovationRecommendation)entity).RecreationAreaRefurbishment;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).SafetyImprovements = ((AccommodationRenovationRecommendation)entity).SafetyImprovements;
            ((AccommodationRenovationRecommendation)accommodationRenovationRecommendation).Comment = ((AccommodationRenovationRecommendation)entity).Comment;

        }

    }
}
