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
    }
}
