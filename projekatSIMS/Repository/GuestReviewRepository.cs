using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class GuestReviewRepository : Repository<GuestReview>
    {
        public override void Edit(Entity entity)
        {
            Entity guestReview = base.Get(entity.Id);

            ((GuestReview)guestReview).AccommodationReservation = ((GuestReview)entity).AccommodationReservation;
            ((GuestReview)guestReview).Cleanliness = ((GuestReview)entity).Cleanliness;
            ((GuestReview)guestReview).respectingRules = ((GuestReview)entity).RespectingRules;
            ((GuestReview)guestReview).Comment = ((GuestReview)entity).Comment;
        }

        public GuestReview GetGuestReviewByAccommodation(int id)
        {
            GuestReview result = null;
            foreach (GuestReview it in DataContext.Instance.GuestReviews)
            {
                if (it.AccommodationReservation.Id == id)
                {
                    result = it;
                }
            }
            return result;
        }


    }
}
