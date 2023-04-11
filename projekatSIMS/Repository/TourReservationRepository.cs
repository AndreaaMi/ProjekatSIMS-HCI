using projekatSIMS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projekatSIMS.Repository
{
    public class TourReservationRepository : Repository<TourReservation>
    {
        public override void Edit(Entity entity)
        {
            Entity tourReservation = base.Get(entity.Id);

            ((TourReservation)tourReservation).Id = ((TourReservation)entity).Id;
            ((TourReservation)tourReservation).TourId = ((TourReservation)entity).TourId;
            ((TourReservation)tourReservation).GuestId = ((TourReservation)entity).GuestId;
            ((TourReservation)tourReservation).NumberOfGuests = ((TourReservation)entity).NumberOfGuests;
        }

        public List<int> GetReservationByGuestId(int guestId)
        {
            List<int> tourReservations = new List<int>();
            foreach(TourReservation tourReservation in DataContext.Instance.TourReservations)
            {
                if(tourReservation.GuestId == guestId)
                {
                    tourReservations.Add(tourReservation.TourId);
                }
            }
            return tourReservations;
        }
    }
}
